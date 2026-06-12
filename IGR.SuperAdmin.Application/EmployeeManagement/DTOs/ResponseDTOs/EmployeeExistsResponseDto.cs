using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.DTOs.ResponseDTOs
{
    public class EmployeeExistsResponseDto
    {
        public string username { get; set; }
        public bool Exists { get; set; }
        public List<string> Suggestions { get; set; } = new();
    }
}
