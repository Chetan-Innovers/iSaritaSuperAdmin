using IGR.SuperAdmin.Application.RoleManagement.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.RoleManagement.Interfaces
{
    public interface IRoleMasterRepository
    {
        Task<IEnumerable<RoleMasterResponseDto>> GetAllAsync();
        Task<RoleMasterResponseDto?> GetByIdAsync(int id, string name);
        Task<RoleMasterResponseDto> CreateAsync(RoleMasterCreateDto dto, long? currentUserId);
        Task<RoleMasterResponseDto?> UpdateAsync(int id, RoleMasterUpdateDto dto, long? currentUserId);
        Task<bool> DeleteAsync(int id, long? currentUserId);
    }
}
