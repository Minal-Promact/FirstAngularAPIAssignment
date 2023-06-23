using System.ComponentModel.DataAnnotations;

namespace FirstAngularAPIAssignment.DTO
{
    public class SkillRequestDTO
    {        
        public int SrNo { get; set; }

        [Required(ErrorMessage = "Please Enter Skill Name.")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Please Select Skill Experience.")]
        public float SkillExperience { get; set; }
        public int EmployeeId { get; set; }
    }
}
