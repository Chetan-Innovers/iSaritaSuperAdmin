using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("village_master")]
    public class VillageMaster
    {
        [Key]
        [Column("pk_villagecode")]
        public long VillageCode { get; set; }

        [Column("e_village_name")]
        public string VillageName { get; set; }

        [Column("fk_talukacode")]
        public int TalukaCode { get; set; }

        [Column("fk_distcode")]
        public int DistCode { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
