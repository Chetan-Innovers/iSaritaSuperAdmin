using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("sromaster")]
    public class SroMaster
    {
        [Column("digcode")]
        public int DigCode { get; set; }

        [Column("jdrcode")]
        public int JdrCode { get; set; }

        [Column("srocode")]
        public int SroCode { get; set; }

        [Column("sroename")]
        public string SroName { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
