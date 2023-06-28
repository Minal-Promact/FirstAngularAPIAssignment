using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FirstAngularAPIAssignment.DTO
{
    public class SkillRequestDTO
    {  

        [Required(ErrorMessage = "Please Enter Skill Name.")]
        [JsonProperty("skillName")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Please Select Skill Experience.")]
        [JsonProperty("skillExperience")]
        public float SkillExperience { get; set; }
        [JsonProperty("employeeId")]
        public int EmployeeId { get; set; }
    }
}
