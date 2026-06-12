using IGR.SuperAdmin.Application.RoleManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.RoleManagement.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.RoleManagement.Services
{
    /// <summary>
    /// Service class for all RoleMaster business logic
    /// Uses IHttpContextAccessor to extract logged-in User ID from JWT token
    /// and delegates data access to IRoleMasterRepository
    /// </summary>
    public class RoleMasterService : IRoleMasterService
    {
        private readonly IRoleMasterRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleMasterService(IRoleMasterRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        // -------------------------------------------------------
        // Helper: Get current logged-in User ID from JWT token
        // JWT mein "UserId" claim se ID nikalte hain
        // -------------------------------------------------------
        private long? GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
            if (long.TryParse(userIdClaim, out var userId))
                return userId;
            return null;
        }

        // -------------------------------------------------------
        // Get all roles (delegates to repository)
        // -------------------------------------------------------
        public async Task<IEnumerable<RoleMasterResponseDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // -------------------------------------------------------
        // Get a role by its ID (delegates to repository)
        // -------------------------------------------------------
        public async Task<RoleMasterResponseDto?> GetByIdAsync(int id, string name)
        {
            return await _repository.GetByIdAsync(id, name);
        }

        // -------------------------------------------------------
        // Create a new role
        // JWT se current User ID nikal ke repository pass karte hain
        // -------------------------------------------------------
        public async Task<RoleMasterResponseDto> CreateAsync(RoleMasterCreateDto dto)
        {
            var currentUserId = GetCurrentUserId(); // JWT se User ID nikalo
            return await _repository.CreateAsync(dto, currentUserId);
        }

        // -------------------------------------------------------
        // Update an existing role
        // JWT se current User ID nikal ke repository pass karte hain
        // -------------------------------------------------------
        public async Task<RoleMasterResponseDto?> UpdateAsync(int id, RoleMasterUpdateDto dto)
        {
            var currentUserId = GetCurrentUserId(); // JWT se User ID nikalo
            return await _repository.UpdateAsync(id, dto, currentUserId);
        }

        // -------------------------------------------------------
        // Soft delete a role
        // -------------------------------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var currentUserId = GetCurrentUserId(); // JWT se User ID nikalo
            return await _repository.DeleteAsync(id, currentUserId);
        }
    }

}
