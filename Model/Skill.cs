using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAngularAPIAssignment.Model
{
    [Table("skill")]
    public class Skill
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       

        [Required(ErrorMessage = "Please Enter Skill Name.")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Please Select Skill Experience.")]
        public float SkillExperience { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
