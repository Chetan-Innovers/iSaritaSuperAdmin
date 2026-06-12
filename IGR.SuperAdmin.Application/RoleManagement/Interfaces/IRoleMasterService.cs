using IGR.SuperAdmin.Application.RoleManagement.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.RoleManagement.Interfaces
{
    /// <summary>
    /// Interface for RoleMaster service
    /// </summary>
    public interface IRoleMasterService
    {
        Task<IEnumerable<RoleMasterResponseDto>> GetAllAsync();
        Task<RoleMasterResponseDto?> GetByIdAsync(int id, string name);
        Task<RoleMasterResponseDto> CreateAsync(RoleMasterCreateDto dto);
        Task<RoleMasterResponseDto?> UpdateAsync(int id, RoleMasterUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
