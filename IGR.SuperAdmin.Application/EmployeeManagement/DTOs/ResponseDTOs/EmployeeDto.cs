using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs
{
    public class EmployeeDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }
        public string MobileNo { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public string Dig { get; set; }
        public string Jdr { get; set; }
        public string Sro { get; set; }

        public DateTime JoinedAt { get; set; }

        public string? District { get; set; }
        public string? Taluka { get; set; }
        public string? Village { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? StateShortName { get; set; }
        public long? Pincode { get; set; }


    }
}
