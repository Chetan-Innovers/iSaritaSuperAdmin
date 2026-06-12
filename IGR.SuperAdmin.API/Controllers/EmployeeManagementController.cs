using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace IGR.SuperAdmin.API.Controllers 
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class EmployeeManagementController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IEmployeeService _empService;
        private readonly IMasterService _master;
        private readonly ILocationService _locationService;


        public EmployeeManagementController(IEmployeeService service, IMasterService master, ILocationService locationService)
        {
            _service = service;
            _master = master;
            _locationService = locationService;
        }

        [HttpGet("exists/{username}")]
        public async Task<IActionResult> EmployeeExists(string username)
        {
            var response = await _service.CheckUsernameAsync(username);
            return Ok(response);
        }
       
        // ✅ REGISTER API
        [HttpPost("register")]
        public async Task<IActionResult> Register(EmployeeRegisterDto dto)
        {
            var result = await _empService.Register(dto);
            return Ok(new
            {
                message = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _empService.GetAll());
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _empService.GetById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, EmployeeRegisterDto dto)
        {
            var result = await _empService.Update(id, dto);

            return Ok(new
            {
                message = result
            });
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _empService.Delete(id);

            return Ok(new
            {
                message = result
            });
        }

        [HttpGet("dig")]
        public async Task<IActionResult> Dig()
           => Ok(await _master.GetDig());

        [HttpGet("jdr/{digCode}")]
        public async Task<IActionResult> Jdr(int digCode)
            => Ok(await _master.GetJdr(digCode));

        [HttpGet("sro/{digCode}/{jdrCode}")]
        public async Task<IActionResult> Sro(int digCode, int jdrCode)
            => Ok(await _master.GetSro(digCode, jdrCode));

        [HttpGet("roles")]
        public async Task<IActionResult> Roles()
            => Ok(await _master.GetRoles());

        [HttpGet("GetByPincode")]
        public async Task<IActionResult> GetByPincode(string pincode)
        {
            var result = await _locationService.GetByPincode(pincode);

            if (result == null)
                return NotFound("Invalid Pincode");

            return Ok(result);
        }
        [HttpGet("security-questions")]
        public async Task<IActionResult> GetSecurityQuestions()
    => Ok(await _master.GetSecurityQuestions());
    }
}

