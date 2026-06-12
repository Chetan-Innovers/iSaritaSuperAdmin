using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.RoleManagement.Models
{
    [Table("role_master", Schema = "public")]
    public class RoleMaster
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("name_en")]
        [MaxLength(200)]
        public string NameEn { get; set; } = string.Empty;

        [Column("name_mr")]
        [MaxLength(400)]
        public string? NameMr { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [Column("deleted_by")]
        public long? DeletedBy { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [Column("version_no")]
        public int VersionNo { get; set; } = 1;
    }

}
