using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("digmaster")]
    public class DigMaster
    {
        [Key]
        [Column("digcode")]
        public int DigCode { get; set; }

        [Column("digename")]
        public string DigName { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
