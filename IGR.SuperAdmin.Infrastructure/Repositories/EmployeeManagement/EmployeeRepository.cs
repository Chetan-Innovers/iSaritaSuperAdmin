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
        .AnyAsync(x => x.username.ToLower() == username.ToLower());
        }
    }
}
