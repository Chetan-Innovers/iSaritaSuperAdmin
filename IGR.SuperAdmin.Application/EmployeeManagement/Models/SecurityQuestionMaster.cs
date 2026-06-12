using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGR.SuperAdmin.Application.EmployeeManagement.Models
{
    [Table("security_question_master")]
    public class SecurityQuestionMaster
    {
        [Key]
        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("question_text")]
        public string QuestionText { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("display_order")]
        public int? DisplayOrder { get; set; }
    }
}
