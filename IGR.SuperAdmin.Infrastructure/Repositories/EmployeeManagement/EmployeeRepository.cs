using IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs;
using IGR.SuperAdmin.Application.EmployeeManagement.Interfaces;
using IGR.SuperAdmin.Application.EmployeeManagement.Models;
using IGR.SuperAdmin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Infrastructure.Repositories.EmployeeManagement
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmployeeExistsAsync(string username)
        {
            return await _context.EmployeeMasters
        .AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<string> Register(EmployeeRegisterDto dto)
        {
            var exists = await _context.EmployeeMasters
                .AnyAsync(x => x.Username == dto.Username);

            if (exists)
                return "Username already exists";

            var emp = new EmployeeMaster
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId,
                MobileNo = dto.MobileNo,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                DigCode = dto.DigCode,
                JdrCode = dto.JdrCode,
                SroCode = dto.SroCode,
                SecurityQuestion = dto.SecurityQuestion,
                SecurityAnswer = dto.SecurityAnswer,
                CurrentAddress = dto.CurrentAddress,
                PermanentAddress = dto.PermanentAddress,
                Pincode = dto.Pincode,
                CreatedAt = DateTime.UtcNow,
                JoinedAt = DateTime.UtcNow
            };

            _context.EmployeeMasters.Add(emp);
            await _context.SaveChangesAsync();

            return "Registered Successfully";
        }

        public async Task<List<EmployeeDto>> GetAll()
        {
            return await (
                from e in _context.EmployeeMasters
                join r in _context.RoleMasters on e.RoleId equals r.RoleId

                join d in _context.DigMasters on e.DigCode equals d.DigCode into dg
                from d in dg.DefaultIfEmpty()

                join j in _context.JdrMasters
                on new { e.DigCode, e.JdrCode }
                equals new { DigCode = (int?)j.DigCode, JdrCode = (int?)j.JdrCode } into jj
                from j in jj.DefaultIfEmpty()

                join s in _context.SroMasters
                on new { e.DigCode, e.JdrCode, e.SroCode }
                equals new { DigCode = (int?)s.DigCode, JdrCode = (int?)s.JdrCode, SroCode = (int?)s.SroCode } into ss
                from s in ss.DefaultIfEmpty()

                join p in _context.PincodeMasters
                on (e.Pincode != null ? e.Pincode.ToString() : null)
                equals p.Pincode into pp
                from p in pp.DefaultIfEmpty()

                select new EmployeeDto
                {
                    Id = e.EmpId,
                    Name = e.FirstName + " " + e.LastName,
                    RoleName = r.NameEn,
                    Dig = d != null ? d.DigName : null,
                    Jdr = j != null ? j.JdrName : null,
                    Sro = s != null ? s.SroName : null,
                    JoinedAt = e.JoinedAt,
                    Pincode = e.Pincode,

                    District = p != null ? p.DistrictName : null,
                    Taluka = p != null ? p.Taluka : null,
                    Village = p != null ? p.VillageName : null,
                    City = p != null ? p.CityName : null,
                    State = p != null ? p.State : null,
                    StateShortName = p != null ? p.StateShortName : null
                }
            ).ToListAsync();
        }

        public async Task<EmployeeDto?> GetById(long id)
        {
            return await (
                from e in _context.EmployeeMasters
                join r in _context.RoleMasters on e.RoleId equals r.RoleId
                where e.EmpId == id

                select new EmployeeDto
                {
                    Id = e.EmpId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Username = e.Username,
                    MobileNo = e.MobileNo,
                    RoleId = e.RoleId,
                    RoleName = r.NameEn,
                    JoinedAt = e.JoinedAt
                }
            ).FirstOrDefaultAsync();
        }

        public async Task<string> Update(long id, EmployeeRegisterDto dto)
        {
            var emp = await _context.EmployeeMasters.FindAsync(id);

            if (emp == null)
                return "Employee not found";

            emp.FirstName = dto.FirstName;
            emp.LastName = dto.LastName;
            emp.MobileNo = dto.MobileNo;
            emp.RoleId = dto.RoleId;
            emp.Username = dto.Username;

            emp.DigCode = dto.DigCode;
            emp.JdrCode = dto.JdrCode;
            emp.SroCode = dto.SroCode;

            emp.CurrentAddress = dto.CurrentAddress;
            emp.PermanentAddress = dto.PermanentAddress;
            emp.Pincode = dto.Pincode;
            emp.SecurityQuestion = dto.SecurityQuestion;
            emp.SecurityAnswer = dto.SecurityAnswer;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                emp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            emp.JoinedAt = dto.JoinedAt;

            await _context.SaveChangesAsync();

            return "Updated Successfully";
        }

        public async Task<string> Delete(long id)
        {
            var emp = await _context.EmployeeMasters.FindAsync(id);

            if (emp == null)
                return "Employee not found";

            _context.EmployeeMasters.Remove(emp);

            await _context.SaveChangesAsync();

            return "Deleted Successfully";
        }
    }
}
