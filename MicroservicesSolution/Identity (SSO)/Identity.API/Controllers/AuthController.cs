using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly Services.ITokenBlacklistService _blacklistService;

        // 1. In-Memory User Store
        // For demonstration purposes, we are saving registered users here.
        // In a real application, this would be a database (like SQL Server).
        // It stores Username as the Key, and Password as the Value.
        private static readonly Dictionary<string, string> _mockDatabase = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, Services.ITokenBlacklistService blacklistService)
        {
            _configuration = configuration;
            _logger = logger;
            _blacklistService = blacklistService;
        }

        // -------------------------------------------------------------
        // REGISTRATION ENDPOINT
        // -------------------------------------------------------------
        [HttpPost("register/citizen")]
        public IActionResult RegisterCitizen([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            // Check if the user already exists in our mock database
            if (_mockDatabase.ContainsKey(request.Username))
            {
                return Conflict(new { message = "Username already exists." });
            }

            // Save the user to the mock database.
            // (In real life, never save plain text passwords. Always hash them!)
            _mockDatabase[request.Username] = request.Password;

            _logger.LogInformation($"New citizen registered: {request.Username}");

            return Ok(new { message = "Citizen registered successfully. You can now login." });
        }

        // -------------------------------------------------------------
        // LOGIN ENDPOINT
        // -------------------------------------------------------------
        [HttpPost("login/citizen")]
        public IActionResult LoginCitizen([FromBody] LoginRequest request)
        {
            // 1. Verify the user exists and the password is correct
            if (_mockDatabase.TryGetValue(request.Username, out string? storedPassword))
            {
                if (storedPassword == request.Password)
                {
                    // 2. If credentials are correct, generate a JWT token.
                    // Notice we are hardcoding the "Citizen" role here.
                    var token = GenerateJwtToken(request.Username, "Citizen");
                    
                    _logger.LogInformation($"Citizen logged in successfully: {request.Username}");

                    // 3. Return the token to the user
                    return Ok(new { token = token, role = "Citizen" });
                }
            }

            // Fallback for the hardcoded admin (for testing)
            if (request.Username?.ToLower() == "admin" && request.Password == "password")
            {
                var token = GenerateJwtToken(request.Username, "Admin");
                return Ok(new { token = token, role = "Admin" });
            }

            // Fallback for the hardcoded citizen (for testing)
            if (request.Username?.ToLower() == "citizen" && request.Password == "password")
            {
                var token = GenerateJwtToken(request.Username, "Citizen");
                return Ok(new { token = token, role = "Citizen" });
            }

            // If credentials fail, return 401 Unauthorized
            return Unauthorized(new { message = "Invalid username or password." });
        }

        // -------------------------------------------------------------
        // LOGOUT ENDPOINT (BLACKLISTING)
        // -------------------------------------------------------------
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // The frontend sends the token in the "Authorization" header
            var authHeader = Request.Headers["Authorization"].ToString();
            
            if (string.IsNullOrWhiteSpace(authHeader))
            {
                return BadRequest(new { message = "No token provided to logout." });
            }

            // Put the token in the garbage bin
            _blacklistService.BlacklistToken(authHeader);

            _logger.LogInformation("A token was successfully blacklisted (User logged out).");

            return Ok(new { message = "Successfully logged out. The token is now invalid." });
        }

        // -------------------------------------------------------------
        // CHECK BLACKLIST (INTERNAL ENDPOINT FOR GATEWAY)
        // -------------------------------------------------------------
        [HttpGet("check-blacklist")]
        public IActionResult CheckBlacklist([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return BadRequest();

            bool isBlacklisted = _blacklistService.IsTokenBlacklisted(token);

            if (isBlacklisted)
            {
                // Return 401 if it's in the garbage bin
                return Unauthorized();
            }

            // Return 200 OK if it's safe
            return Ok();
        }

        // -------------------------------------------------------------
        // JWT GENERATOR (HOW IT WORKS)
        // -------------------------------------------------------------
        private string GenerateJwtToken(string username, string role)
        {
            // 1. Get the secret key from appsettings.json. This key is used to "sign" the token securely.
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

            // 2. Create "Claims". Claims are pieces of information about the user hidden inside the token.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                // IMPORTANT: This is the role-based claim! 
                // When the Gateway reads this token, it will look for this exact claim to see if the user is a "Citizen".
                new Claim(ClaimTypes.Role, role)
            };

            // 3. Build the token descriptor (the blueprint for the token)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token lasts for 7 days
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                // Sign the token using our secret key so hackers can't modify it.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Actually generate and return the string token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
