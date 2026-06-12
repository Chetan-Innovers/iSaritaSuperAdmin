using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("employee_master")]
    public class EmployeeMaster
    {
        [Key]
        [Column("emp_id")]
        public long EmpId { get; set; }

        [Column("honorific")]
        public string? Honorific { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("full_name_mr")]
        public string? FullNameMr { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }

        [Column("department")]
        public string? Department { get; set; }

        [Column("sro_code")]
        public int? SroCode { get; set; }

        [Column("jdr_code")]
        public int? JdrCode { get; set; }

        [Column("dig_code")]
        public int? DigCode { get; set; }

        [Column("dist_code")]
        public int? DistCode { get; set; }

        [Column("joined_at")]
        public DateTime JoinedAt { get; set; }

        [Column("relieved_at")]
        public DateTime? RelievedAt { get; set; }

        [Column("mobile_no")]
        public string MobileNo { get; set; }

        [Column("official_email")]
        public string? OfficialEmail { get; set; }

        [Column("personal_email")]
        public string? PersonalEmail { get; set; }

        [Column("pan_number")]
        public string? PanNumber { get; set; }

        [Column("aadhaar_token")]
        public string? AadhaarToken { get; set; }

        [Column("govt_emp_id")]
        public string? GovtEmpId { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("login_attempt_count")]
        public short LoginAttemptCount { get; set; }

        [Column("is_locked")]
        public bool IsLocked { get; set; }

        [Column("locked_at")]
        public DateTimeOffset? LockedAt { get; set; }

        [Column("last_login_at")]
        public DateTimeOffset? LastLoginAt { get; set; }

        [Column("last_login_ip")]
        public string? LastLoginIp { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("deleted_at")]
        public DateTimeOffset? DeletedAt { get; set; }

        [Column("deleted_by")]
        public long? DeletedBy { get; set; }

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [Column("version_no")]
        public int VersionNo { get; set; }

        [Column("current_address")]
        public string? CurrentAddress { get; set; }

        [Column("permanent_address")]
        public string? PermanentAddress { get; set; }

        [Column("pincode")]
        public long? Pincode { get; set; }

        [Column("security_question")]
        public string? SecurityQuestion { get; set; }

        [Column("security_answer")]
        public string? SecurityAnswer { get; set; }
    }
}
