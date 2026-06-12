using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.RoleManagement.DTOs.ResponseDTOs
{
    /// <summary>
    /// DTO used for GET response - returns ALL fields from role_master table
    /// </summary>
    public class RoleMasterResponseDto
    {
        public int RoleId { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string? NameMr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int VersionNo { get; set; }
    }

    /// <summary>
    /// DTO used for POST (create) request body
    /// </summary>
    public class RoleMasterCreateDto
    {
        public string NameEn { get; set; } = string.Empty;
        public string? NameMr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// DTO used for PUT (update) request body
    /// </summary>
    public class RoleMasterUpdateDto
    {
        public string NameEn { get; set; } = string.Empty;
        public string? NameMr { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

}
