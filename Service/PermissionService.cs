using worksquare.Data;
using worksquare.Model;
using Microsoft.EntityFrameworkCore;
using worksquare.Enum;

namespace worksquare.Service
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(int userId, int companyId, CompanyRoleEnum role, string permissionName);
    }

    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _context;

        public PermissionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasPermissionAsync(int userId, int companyId, CompanyRoleEnum role, string permissionName)
        {
            // Evaluation order: ExplicitDeny > ExplicitGrant > RoleDefault > Deny

            var userPermission = await _context.UserPermissions
                .Where(up => up.UserId == userId && up.CompanyId == companyId && up.Permission == permissionName)
                .Select(up => new { up.IsGranted, up.ExpiresAt })
                .FirstOrDefaultAsync();

            if (userPermission != null)
            {
                // Check if expired
                if (userPermission.ExpiresAt.HasValue && userPermission.ExpiresAt.Value < DateTime.UtcNow)
                {
                    // Expired override is ignored. Proceed to role default.
                }
                else
                {
                    // If IsGranted = false  -> DENY (wins)
                    // If IsGranted = true   -> GRANT (wins)
                    return userPermission.IsGranted;
                }
            }

            // Fallback to role default
            // Role Permission might be company-specific (CompanyId > 0) or platform-wide (CompanyId = 0)
            
            var rolePermission = await _context.RolePermissions
                .Where(rp => rp.Role == role && rp.Permission == permissionName && (rp.CompanyId == companyId || rp.CompanyId == 0))
                .OrderByDescending(rp => rp.CompanyId) // Prefer company-specific (CompanyId > 0) over platform-wide (CompanyId = 0)
                .FirstOrDefaultAsync();

            if (rolePermission != null)
            {
                return true;
            }

            return false;
        }
    }
}
