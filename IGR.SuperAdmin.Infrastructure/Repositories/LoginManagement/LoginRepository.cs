using IGR.SuperAdmin.Application.Authentication.Interfaces;
using IGR.SuperAdmin.Application.EmployeeManagement.Models;
using IGR.SuperAdmin.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Infrastructure.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;

        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public EmployeeMaster? GetEmployeeByUsername(string username)
        {
            return _context.EmployeeMasters
                .FirstOrDefault(x =>
                    x.Username == username &&
                    x.IsActive &&
                    !x.IsDeleted);
        }

        public string? GetRoleNameById(int roleId)
        {
            return _context.RoleMasters
                .Where(x => x.RoleId == roleId)
                .Select(x => x.NameEn)
                .FirstOrDefault();
        }

        public void UpdateEmployee(EmployeeMaster employee)
        {
            _context.SaveChanges();
        }
    }
}
