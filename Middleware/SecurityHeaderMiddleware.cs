namespace worksquare.Middleware
{
    /// <summary>
    /// Appends standard security headers to every HTTP response payload.
    /// </summary>
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Apply headers before the response starts sending
            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;
                
                if (!headers.ContainsKey("X-Frame-Options"))
                    headers.Append("X-Frame-Options", "DENY");
                    
                if (!headers.ContainsKey("X-Content-Type-Options"))
                    headers.Append("X-Content-Type-Options", "nosniff");
                    
                if (!headers.ContainsKey("X-XSS-Protection"))
                    headers.Append("X-XSS-Protection", "1; mode=block");

                if (!headers.ContainsKey("Strict-Transport-Security"))
                    headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");

                if (!headers.ContainsKey("Content-Security-Policy"))
                    headers.Append("Content-Security-Policy", "default-src 'self'; frame-ancestors 'none';");
                
                if (!headers.ContainsKey("Referrer-Policy"))
                    headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
