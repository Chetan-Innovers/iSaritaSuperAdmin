using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs
{
    public class LocationResponseDto
    {
        public string District { get; set; }
        public string Taluka { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateShortName { get; set; }
    }
}
