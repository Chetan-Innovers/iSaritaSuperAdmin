using IGR.SuperAdmin.Application.EmployeeManagement.Models;
using IGR.SuperAdmin.Application.RoleManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeMaster>().ToTable("employee_master");
            modelBuilder.Entity<RoleMaster>().ToTable("role_master");
            base.OnModelCreating(modelBuilder);
        }
    }
}
