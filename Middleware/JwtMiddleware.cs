using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using worksquare.Data;

namespace worksquare.Middleware
{
    /// <summary>
    /// Runs after JWT Bearer authentication has validated the token signature.
    /// Performs DB-backed checks on every authenticated request using the ACCESS token:
    ///
    ///   1. userId claim → User must exist in DB
    ///   2. companyId claim → CompanyUser entry must exist (for non-system users)
    ///   3. user.IsActive → 403 if false
    ///   4. Token revocation (access token) — we rely on short expiry (15 min) instead of
    ///      JTI DB lookup; full revocation is enforced at the refresh-token layer.
    ///
    /// NOTE: Role-based access (point 4 in spec) is enforced per-endpoint via
    ///       [Authorize(Roles = "...")] attributes, not here centrally, because
    ///       different endpoints accept different roles.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext db)
        {
            // Only check authenticated requests (i.e. Bearer token present + valid signature)
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            // ── 1. Extract claims ─────────────────────────────────────────────────
            var userIdStr    = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                            ?? context.User.FindFirst("sub")?.Value;
            var companyIdStr = context.User.FindFirst("companyId")?.Value;
            var userType     = context.User.FindFirst("userType")?.Value;

            if (!int.TryParse(userIdStr, out var userId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid token: missing userId." });
                return;
            }

            // ── 2. Verify userId exists in DB ─────────────────────────────────────
            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "User not found." });
                return;
            }

            // ── 3. Verify user.IsActive ───────────────────────────────────────────
            if (!user.IsActive)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "Account is inactive." });
                return;
            }

            // ── 4. Verify companyId exists in DB (CompanyUser only) ────────────────
            if (userType == "CompanyUser")
            {
                if (!int.TryParse(companyIdStr, out var companyId))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { message = "Invalid token: missing companyId." });
                    return;
                }

                var companyUserExists = await db.CompanyUsers
                    .AsNoTracking()
                    .AnyAsync(cu => cu.UserId == userId && cu.CompanyId == companyId);

                if (!companyUserExists)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { message = "Company association not found." });
                    return;
                }
            }

            await _next(context);
        }
    }
}
