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

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("username")]
        public string username { get; set; }
    }
}
