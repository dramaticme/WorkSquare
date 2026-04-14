using Microsoft.EntityFrameworkCore;
using worksquare.Data;
using worksquare.DTO;
using worksquare.Model;

namespace worksquare.Service
{
    /// <summary>
    /// Handles login, logout, and token rotation (refresh).
    /// </summary>
    public class AuthService
    {
        private readonly AppDbContext   _db;
        private readonly TokenService   _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext db, TokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _db                  = db;
            _tokenService        = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Login

        /// <summary>
        /// Validates credentials, checks IsActive, resolves role, issues token pair,
        /// persists refresh token + session.
        /// Returns null if credentials are invalid or user is inactive.
        /// </summary>
        public async Task<TokenResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var user = await _db.Users
                .Include(u => u.CompanyUser)
                .Include(u => u.SystemUser)
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user is null) return null;

            // Simple password check — replace with BCrypt / PBKDF2 when hashing is added
            if (user.Password != dto.Password) return null;

            if (!user.IsActive) return null;

            // Resolve company + role
            int    companyId;
            string role;
            string userType;

            if (user.CompanyUser is not null)
            {
                companyId = user.CompanyUser.CompanyId;
                role      = user.CompanyUser.Role.ToString();
                userType  = "CompanyUser";
            }
            else if (user.SystemUser is not null)
            {
                companyId = 0;   // system users have no tenant company
                role      = user.SystemUser.Role.ToString();
                userType  = "SystemUser";
            }
            else
            {
                return null;    // orphaned user — no role association
            }

            var ip = GetClientIp();

            // Issue tokens
            var accessToken                        = _tokenService.CreateAccessToken(user, companyId, role, userType);
            var (refreshTokenStr, jti, expiresAt)  = _tokenService.CreateRefreshToken(user, companyId);

            // Persist refresh token (whitelist)
            var refreshToken = new RefreshToken
            {
                Jti       = jti,
                Token     = refreshTokenStr,
                UserId    = user.Id,
                CompanyId = companyId,
                DeviceIp  = ip,
                ExpiresAt = expiresAt
            };
            _db.RefreshTokens.Add(refreshToken);

            // Persist session
            var session = new UserSession
            {
                UserId           = user.Id,
                CompanyId        = companyId,
                RefreshTokenJti  = jti,
                DeviceIp         = ip
            };
            _db.UserSessions.Add(session);

            await _db.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken           = accessToken,
                RefreshToken          = refreshTokenStr,
                AccessTokenExpiresAt  = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiresAt = expiresAt
            };
        }

        // Refresh

        /// <summary>
        /// Validates the incoming refresh token against the DB whitelist,
        /// checks user IsActive + userId/companyId existence,
        /// rotates the token (old revoked, new issued).
        /// </summary>
        public async Task<TokenResponseDTO?> RefreshAsync(string incomingRefreshToken)
        {
            // 1. Validate JWT signature + expiry first (no DB hit yet)
            var principal = _tokenService.ValidateTokenSignature(incomingRefreshToken);
            if (principal is null) return null;

            // 2. Extract claims
            var userIdStr    = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                            ?? principal.FindFirst("sub")?.Value;
            var companyIdStr = principal.FindFirst("companyId")?.Value;
            var jti          = principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti)?.Value;

            if (!int.TryParse(userIdStr, out var userId)
             || !int.TryParse(companyIdStr, out var companyId)
             || string.IsNullOrEmpty(jti))
                return null;

            // 3. Whitelist check — token must exist and not be revoked
            var stored = await _db.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Jti == jti && !rt.IsRevoked);
            if (stored is null) return null;

            // 4. Verify token string hasn't been tampered with
            if (stored.Token != incomingRefreshToken) return null;

            // 5. Check user + company still exist in DB
            var user = await _db.Users
                .Include(u => u.CompanyUser)
                .Include(u => u.SystemUser)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null || !user.IsActive) return null;

            // Ensure the companyId in the token still matches (tenant isolation)
            var validCompany = (user.CompanyUser?.CompanyId == companyId)
                            || (companyId == 0 && user.SystemUser is not null);
            if (!validCompany) return null;

            // Resolve role
            string role;
            string userType;
            if (user.CompanyUser is not null)
            {
                role     = user.CompanyUser.Role.ToString();
                userType = "CompanyUser";
            }
            else
            {
                role     = user.SystemUser!.Role.ToString();
                userType = "SystemUser";
            }

            var ip = GetClientIp();

            // 6. Revoke old refresh token
            stored.IsRevoked = true;

            // 7. Deactivate old session
            var oldSession = await _db.UserSessions
                .FirstOrDefaultAsync(s => s.RefreshTokenJti == jti);
            if (oldSession is not null) oldSession.IsActive = false;

            // 8. Issue new token pair
            var newAccessToken                          = _tokenService.CreateAccessToken(user, companyId, role, userType);
            var (newRefreshTokenStr, newJti, expiresAt) = _tokenService.CreateRefreshToken(user, companyId);

            // 9. Persist new refresh token
            var newRefreshToken = new RefreshToken
            {
                Jti       = newJti,
                Token     = newRefreshTokenStr,
                UserId    = user.Id,
                CompanyId = companyId,
                DeviceIp  = ip,
                ExpiresAt = expiresAt
            };
            _db.RefreshTokens.Add(newRefreshToken);

            // 10. Persist new session
            var newSession = new UserSession
            {
                UserId          = user.Id,
                CompanyId       = companyId,
                RefreshTokenJti = newJti,
                DeviceIp        = ip
            };
            _db.UserSessions.Add(newSession);

            await _db.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken           = newAccessToken,
                RefreshToken          = newRefreshTokenStr,
                AccessTokenExpiresAt  = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiresAt = expiresAt
            };
        }

        // Logout

        /// <summary>
        /// Revokes the specified refresh token and deactivates its session.
        /// The associated short-lived access token will expire naturally (≤15 min).
        /// </summary>
        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var principal = _tokenService.ValidateTokenSignature(refreshToken);
            if (principal is null) return false;

            var jti = principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti)?.Value;
            if (string.IsNullOrEmpty(jti)) return false;

            var stored = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Jti == jti);
            if (stored is null) return false;

            stored.IsRevoked = true;

            var session = await _db.UserSessions
                .FirstOrDefaultAsync(s => s.RefreshTokenJti == jti);
            if (session is not null) session.IsActive = false;

            await _db.SaveChangesAsync();
            return true;
        }

        // Helpers

        private string GetClientIp()
        {
            var ctx = _httpContextAccessor.HttpContext;
            return ctx?.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }
    }
}
