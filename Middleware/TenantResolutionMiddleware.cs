using System.Security.Claims;

namespace worksquare.Middleware
{
    /// <summary>
    /// Extracts the CompanyId from the authenticated user's JWT 
    /// and makes it accessible for the current request pipeline.
    /// </summary>
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var companyIdStr = context.User.FindFirst("companyId")?.Value;
                if (!string.IsNullOrEmpty(companyIdStr) && int.TryParse(companyIdStr, out var companyId))
                {
                    context.Items["CompanyId"] = companyId;
                }
            }

            await _next(context);
        }
    }
}
