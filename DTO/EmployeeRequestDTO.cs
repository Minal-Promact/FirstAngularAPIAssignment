using FirstAngularAPIAssignment.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FirstAngularAPIAssignment.DTO
{
    public class EmployeeRequestDTO
    {
        [Key, Required]
        //[JsonProperty("id")]
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Name should be maximum 30 length.")]
        [Required(ErrorMessage = "Please Enter Employee Name.")]
        [RegularExpression(pattern: "[a-zA-Z ]*$", ErrorMessage = "Please enter only alphabets.")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select Gender.")]
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [StringLength(10, ErrorMessage = "ContactNumber should be maximum 30 length.")]
        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [JsonProperty("email")]
        public string Email { get; set; }

        public List<SkillRequestDTO> Skills { get; set; } = new List<SkillRequestDTO>();
    }
}
