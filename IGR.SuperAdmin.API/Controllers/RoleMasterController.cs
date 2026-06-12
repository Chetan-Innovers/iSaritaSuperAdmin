using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using IGR.SuperAdmin.Application.RoleManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.RoleManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGR.SuperAdmin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleMasterController : ControllerBase
    {
        private readonly IRoleMasterService _service;

        public RoleMasterController(IRoleMasterService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleMasterResponseDto>), 200)]
        public async Task<ActionResult<IEnumerable<RoleMasterResponseDto>>> GetRoleMasters()
        {
            var roles = await _service.GetAllAsync();
            return Ok(roles);
        }

        // GET: api/RoleMaster/{id}
        [HttpGet("{id}/{name}")]
        [ProducesResponseType(typeof(RoleMasterResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoleMasterResponseDto>> GetRoleMaster(int id, string name)
        {
            var role = await _service.GetByIdAsync(id, name);
            if (role == null)
                return NotFound(new { message = $"Role with ID {id} not found." });

            return Ok(role);
        }

        // POST: api/RoleMaster
        [HttpPost]
        [ProducesResponseType(typeof(RoleMasterResponseDto), 201)]
        public async Task<ActionResult<RoleMasterResponseDto>> PostRoleMaster([FromBody] RoleMasterCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetRoleMaster), new { id = created.RoleId }, created);
        }

        // PUT: api/RoleMaster/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RoleMasterResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoleMasterResponseDto>> PutRoleMaster(int id, [FromBody] RoleMasterUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Role with ID {id} not found." });

            return Ok(updated);
        }

        // DELETE: api/RoleMaster/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRoleMaster(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = $"Role with ID {id} not found." });

            return Ok(new { message = $"Role with ID {id} deleted successfully." });
        }
    }
}
