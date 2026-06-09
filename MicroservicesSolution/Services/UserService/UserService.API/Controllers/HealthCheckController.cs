using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { Status = "Healthy", Service = "UserService.API" });
}
