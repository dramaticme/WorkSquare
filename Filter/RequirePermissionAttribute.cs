using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using worksquare.Service;
using worksquare.Enum;
using System.Security.Claims;

namespace worksquare.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RequirePermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;

        public RequirePermissionAttribute(string permission)
        {
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Look for userId from claims
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var companyIdClaim = user.FindFirst("companyId")?.Value;
            var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId) ||
                string.IsNullOrEmpty(companyIdClaim) || !int.TryParse(companyIdClaim, out int companyId) ||
                string.IsNullOrEmpty(roleClaim) || !System.Enum.TryParse<CompanyRoleEnum>(roleClaim, out CompanyRoleEnum role))
            {
                context.Result = new ForbidResult();
                return;
            }

            var permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();

            var hasPermission = await permissionService.HasPermissionAsync(userId, companyId, role, _permission);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
