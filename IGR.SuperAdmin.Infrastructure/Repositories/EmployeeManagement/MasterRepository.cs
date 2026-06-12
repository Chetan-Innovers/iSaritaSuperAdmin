using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using IGR.SuperAdmin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Infrastructure.Repositories.EmployeeManagement
{
    public class MasterRepository : IMasterRepository
    {
        private readonly AppDbContext _context;

        public MasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DropdownDto>> GetDig()
        {
            return await _context.DigMasters
                .Where(x => !x.IsDeleted)
                .Select(x => new DropdownDto
                {
                    Id = x.DigCode,
                    Name = x.DigName
                }).ToListAsync();
        }

        public async Task<List<DropdownDto>> GetJdr(int digCode)
        {
            return await _context.JdrMasters
                .Where(x => x.DigCode == digCode && !x.IsDeleted)
                .Select(x => new DropdownDto
                {
                    Id = x.JdrCode,
                    Name = x.JdrName
                }).ToListAsync();
        }

        public async Task<List<DropdownDto>> GetSro(int digCode, int jdrCode)
        {
            return await _context.SroMasters
                .Where(x => x.DigCode == digCode && x.JdrCode == jdrCode && !x.IsDeleted)
                .Select(x => new DropdownDto
                {
                    Id = x.SroCode,
                    Name = x.SroName
                }).ToListAsync();
        }

        public async Task<List<DropdownDto>> GetRoles()
        {
            return await _context.RoleMasters
                .Where(x => x.IsActive)
                .Select(x => new DropdownDto
                {
                    Id = x.RoleId,
                    Name = x.NameEn
                }).ToListAsync();
        }

        public async Task<List<DropdownDto>> GetSecurityQuestions()
        {
            return await _context.SecurityQuestionMasters
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new DropdownDto
                {
                    Id = x.QuestionId,
                    Name = x.QuestionText
                })
                .ToListAsync();
        }
    }
}
