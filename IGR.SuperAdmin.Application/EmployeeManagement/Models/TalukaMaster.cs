using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("taluka_master")]
    public class TalukaMaster
    {
        [Key]
        [Column("pk_talukacode")]
        public int TalukaCode { get; set; }

        [Column("e_taluka_name")]
        public string TalukaName { get; set; }

        [Column("fk_distcode")]
        public int DistCode { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
