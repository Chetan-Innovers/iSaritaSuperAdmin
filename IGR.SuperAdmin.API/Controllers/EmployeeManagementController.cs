using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGR.SuperAdmin.API.Controllers 
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class UsernameVerify : ControllerBase
    {
        private readonly IEmployeeService _service;

        public UsernameVerify(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("exists/{username}")]
        public async Task<IActionResult> EmployeeExists(string username)
        {
            var response = await _service.CheckUsernameAsync(username);
            return Ok(response);
        }
    }
}
