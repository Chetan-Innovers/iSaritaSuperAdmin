using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("jdrmaster")]
    public class JdrMaster
    {
        [Column("digcode")]
        public int DigCode { get; set; }

        [Column("jdrcode")]
        public int JdrCode { get; set; }

        [Column("jdrename")]
        public string JdrName { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
