using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using worksquare.DTO;
using worksquare.Service;

namespace worksquare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST /api/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result is null)
                return Unauthorized(new { message = "Invalid credentials or inactive account." });

            return Ok(result);
        }

        // POST /api/auth/refresh
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO dto)
        {
            var result = await _authService.RefreshAsync(dto.RefreshToken);
            if (result is null)
                return Unauthorized(new { message = "Refresh token is invalid, expired, or revoked." });

            return Ok(result);
        }

        // POST /api/auth/logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDTO dto)
        {
            var success = await _authService.LogoutAsync(dto.RefreshToken);
            if (!success)
                return BadRequest(new { message = "Logout failed: token not found or already revoked." });

            return Ok(new { message = "Logged out successfully." });
        }

        // GET /api/auth/me  — example of a protected endpoint with role check
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                userId    = User.FindFirst("sub")?.Value
                          ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                companyId = User.FindFirst("companyId")?.Value,
                role      = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value,
                userType  = User.FindFirst("userType")?.Value
            });
        }
    }
}
