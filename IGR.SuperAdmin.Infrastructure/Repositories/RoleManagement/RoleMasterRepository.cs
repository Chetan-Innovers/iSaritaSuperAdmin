using IGR.SuperAdmin.Application.RoleManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.RoleManagement.Interfaces;
using IGR.SuperAdmin.Application.RoleManagement.Models;
using IGR.SuperAdmin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Infrastructure.Repositories.RoleManagement
{
    public class RoleMasterRepository : IRoleMasterRepository
    {
        private readonly AppDbContext _context;

        public RoleMasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleMasterResponseDto>> GetAllAsync()
        {
            return await _context.RoleMasters
                .Select(r => new RoleMasterResponseDto
                {
                    RoleId = r.RoleId,
                    NameEn = r.NameEn,
                    NameMr = r.NameMr,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    IsDeleted = r.IsDeleted,
                    DeletedAt = r.DeletedAt,
                    DeletedBy = r.DeletedBy,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    CreatedBy = r.CreatedBy,
                    UpdatedBy = r.UpdatedBy,
                    VersionNo = r.VersionNo
                })
                .ToListAsync();
        }

        public async Task<RoleMasterResponseDto?> GetByIdAsync(int id, string name)
        {
            return await _context.RoleMasters
                .Where(r => r.RoleId == id)
                .Select(r => new RoleMasterResponseDto
                {
                    RoleId = r.RoleId,
                    NameEn = r.NameEn,
                    NameMr = r.NameMr,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    IsDeleted = r.IsDeleted,
                    DeletedAt = r.DeletedAt,
                    DeletedBy = r.DeletedBy,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    CreatedBy = r.CreatedBy,
                    UpdatedBy = r.UpdatedBy,
                    VersionNo = r.VersionNo
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RoleMasterResponseDto> CreateAsync(RoleMasterCreateDto dto, long? currentUserId)
        {
            var roleMaster = new RoleMaster
            {
                NameEn = dto.NameEn,
                NameMr = dto.NameMr,
                Description = dto.Description,
                IsActive = dto.IsActive,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = currentUserId,
                VersionNo = 1
            };

            _context.RoleMasters.Add(roleMaster);
            await _context.SaveChangesAsync();

            return new RoleMasterResponseDto
            {
                RoleId = roleMaster.RoleId,
                NameEn = roleMaster.NameEn,
                NameMr = roleMaster.NameMr,
                Description = roleMaster.Description,
                IsActive = roleMaster.IsActive,
                IsDeleted = roleMaster.IsDeleted,
                CreatedAt = roleMaster.CreatedAt,
                UpdatedAt = roleMaster.UpdatedAt,
                CreatedBy = roleMaster.CreatedBy,
                UpdatedBy = roleMaster.UpdatedBy,
                VersionNo = roleMaster.VersionNo
            };
        }

        public async Task<RoleMasterResponseDto?> UpdateAsync(int id, RoleMasterUpdateDto dto, long? currentUserId)
        {
            var existingRole = await _context.RoleMasters
                .FirstOrDefaultAsync(r => r.RoleId == id && !r.IsDeleted);

            if (existingRole == null) return null;

            existingRole.NameEn = dto.NameEn;
            existingRole.NameMr = dto.NameMr;
            existingRole.Description = dto.Description;
            existingRole.IsActive = dto.IsActive;
            existingRole.UpdatedAt = DateTime.UtcNow;
            existingRole.UpdatedBy = currentUserId;
            existingRole.VersionNo += 1;

            await _context.SaveChangesAsync();

            return new RoleMasterResponseDto
            {
                RoleId = existingRole.RoleId,
                NameEn = existingRole.NameEn,
                NameMr = existingRole.NameMr,
                Description = existingRole.Description,
                IsActive = existingRole.IsActive,
                IsDeleted = existingRole.IsDeleted,
                CreatedAt = existingRole.CreatedAt,
                UpdatedAt = existingRole.UpdatedAt,
                CreatedBy = existingRole.CreatedBy,
                UpdatedBy = existingRole.UpdatedBy,
                VersionNo = existingRole.VersionNo
            };
        }

        public async Task<bool> DeleteAsync(int id, long? currentUserId)
        {
            var roleMaster = await _context.RoleMasters
                .FirstOrDefaultAsync(r => r.RoleId == id && !r.IsDeleted);

            if (roleMaster == null) return false;

            roleMaster.IsDeleted = true;
            roleMaster.IsActive = false;
            roleMaster.DeletedAt = DateTime.UtcNow;
            roleMaster.DeletedBy = currentUserId;
            roleMaster.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
