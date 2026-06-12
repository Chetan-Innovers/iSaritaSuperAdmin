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
        
        public DbSet<SroMaster> SroMasters { get; set; }
        public DbSet<JdrMaster> JdrMasters { get; set; }
        public DbSet<DigMaster> DigMasters { get; set; }
        public DbSet<DistrictMaster> DistrictMasters { get; set; }
        public DbSet<TalukaMaster> TalukaMasters { get; set; }
        public DbSet<VillageMaster> VillageMasters { get; set; }
        public DbSet<PincodeMaster> PincodeMasters { get; set; }
        public DbSet<SecurityQuestionMaster> SecurityQuestionMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeMaster>().ToTable("employee_master");
            modelBuilder.Entity<RoleMaster>().ToTable("role_master");
            base.OnModelCreating(modelBuilder);
            // ✅ Composite Keys
            modelBuilder.Entity<JdrMaster>()
                .HasKey(x => new { x.DigCode, x.JdrCode });

            modelBuilder.Entity<SroMaster>()
                .HasKey(x => new { x.DigCode, x.JdrCode, x.SroCode });
            //modelBuilder.Entity<RoleMaster>(entity =>
            //{
            //    entity.ToTable("rolemaster");

            //    entity.HasKey(r => r.RoleId);

            //    entity.Property(r => r.RoleId).HasColumnName("roleid");
            //    entity.Property(r => r.RoleName).HasColumnName("rolename");

            //    entity.Property(r => r.IsActive).HasColumnName("isactive");
            //});

            modelBuilder.Entity<PincodeMaster>()
       .ToTable("pincode_master");
        }
    }
}
