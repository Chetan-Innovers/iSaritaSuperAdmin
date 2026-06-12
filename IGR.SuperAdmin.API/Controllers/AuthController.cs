using IGR.SuperAdmin.Application.Authentication.Interfaces;
using IGR.SuperAdmin.Application.Authentication.Models;
using IGR.SuperAdmin.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IGR.SuperAdmin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly ILoginService _loginService;

        public AuthController(IConfiguration configuration, AppDbContext context, ILoginService loginService)
        {
            _configuration = configuration;
            _context = context;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request is null");
            }
            var result = _loginService.Login(request);

            if (!result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Logout()
        {
            var username = User.Identity?.Name ?? "User";

            return Ok(new
            {
                Success = true,
                Message = "Logout successful.",
                User = username,
                LoggedOutAt = DateTime.UtcNow
            });
        }
    }
}
