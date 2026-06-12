using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs
{
    public class EmployeeRegisterDto
    {
        public string? Honorific { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullNameMr { get; set; }

        // Role & Posting
        public int RoleId { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public int? SroCode { get; set; }
        public int? JdrCode { get; set; }
        public int? DigCode { get; set; }
        public int? DistCode { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime? RelievedAt { get; set; }

        // Contact
        public string MobileNo { get; set; }
        public string? OfficialEmail { get; set; }
        public string? PersonalEmail { get; set; }

        // Govt IDs
        public string? PanNumber { get; set; }
        public string? AadhaarToken { get; set; }
        public string? GovtEmpId { get; set; }

        // Login
        public string Username { get; set; }
        public string Password { get; set; }

        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }

        public long? Pincode { get; set; }

        // Audit
        public long? CreatedBy { get; set; }


    }
}
