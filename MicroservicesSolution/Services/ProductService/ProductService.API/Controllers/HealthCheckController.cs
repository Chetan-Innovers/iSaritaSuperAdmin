using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.API.Controllers;

[ApiController]
[Authorize(Roles = "Citizen")]
[Route("[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { Status = "Healthy", Service = "ProductService.API" });
}
