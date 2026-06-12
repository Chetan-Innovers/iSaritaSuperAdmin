using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("pincode_master")]
    public class PincodeMaster
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("pincode")]
        public string? Pincode { get; set; }

        [Column("districtname")]
        public string? DistrictName { get; set; }

        [Column("taluka")] // ✅ FIXED (your table has taluka)
        public string? Taluka { get; set; }

        [Column("cityname")]
        public string? CityName { get; set; }

        [Column("village_name")]
        public string? VillageName { get; set; }

        [Column("state")]
        public string? State { get; set; }

        [Column("stateshortname")]
        public string? StateShortName { get; set; }
    }
}
