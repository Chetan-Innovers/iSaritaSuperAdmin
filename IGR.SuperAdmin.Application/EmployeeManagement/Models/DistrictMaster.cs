using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("district_master")]
    public class DistrictMaster
    {
        [Key]
        [Column("pk_distcode")]
        public long DistCode { get; set; }

        [Column("e_dist_name")]
        public string DistName { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
