using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using worksquare.Enum;
using worksquare.Model;

namespace worksquare.Service
{
    /// <summary>
    /// Responsible for creating and validating JWT access and refresh tokens.
    ///
    /// Strategy:
    ///   • Access token  – 15 min, validated purely by signature + expiry (no DB check).
    ///   • Refresh token – 7 days, JTI is stored in DB (whitelist). Rotation on every use.
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        // ─── Access Token ─────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a 15-minute signed JWT access token.
        /// Claims: sub (userId), companyId, role, userType.
        /// No JTI — short expiry is the revocation mechanism.
        /// </summary>
        public string CreateAccessToken(User user, int companyId, string role, string userType)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,        user.Id.ToString()),
                new Claim("companyId",                        companyId.ToString()),
                new Claim(ClaimTypes.Role,                    role),
                new Claim("userType",                         userType),   // "CompanyUser" | "SystemUser"
                new Claim(JwtRegisteredClaimNames.Iat,
                          DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64)
            };

            var expiry = DateTime.UtcNow.AddMinutes(15);
            return BuildToken(claims, expiry);
        }

        // ─── Refresh Token ────────────────────────────────────────────────────────

        /// <summary>
        /// Creates a 7-day signed JWT refresh token.
        /// Claims: sub (userId), companyId, jti (GUID – stored in DB whitelist).
        /// </summary>
        public (string Token, string Jti, DateTime ExpiresAt) CreateRefreshToken(User user, int companyId)
        {
            var jti     = Guid.NewGuid().ToString();
            var expires = DateTime.UtcNow.AddDays(7);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("companyId",                 companyId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iat,
                          DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64)
            };

            return (BuildToken(claims, expires), jti, expires);
        }

        // ─── Validation ───────────────────────────────────────────────────────────

        /// <summary>
        /// Validates a token's signature and expiry WITHOUT requiring the DB.
        /// Used for refresh-token validation before the DB whitelist check.
        /// </summary>
        public ClaimsPrincipal? ValidateTokenSignature(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer           = true,
                ValidIssuer              = _config["Jwt:Issuer"],
                ValidateAudience         = true,
                ValidAudience            = _config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = key,
                ValidateLifetime         = true,
                ClockSkew                = TimeSpan.Zero
            };

            try
            {
                var handler    = new JwtSecurityTokenHandler();
                var principal  = handler.ValidateToken(token, validationParams, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        // ─── Helpers ──────────────────────────────────────────────────────────────

        private string BuildToken(Claim[] claims, DateTime expiry)
        {
            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:             _config["Jwt:Issuer"],
                audience:           _config["Jwt:Audience"],
                claims:             claims,
                notBefore:          DateTime.UtcNow,
                expires:            expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
