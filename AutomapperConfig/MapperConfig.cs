using AutoMapper;
using FirstAngularAPIAssignment.DTO;
using FirstAngularAPIAssignment.Model;

namespace FirstAngularAPIAssignment.AutomapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee, EmployeeRequestDTO>().ReverseMap();
            CreateMap<Employee, EmployeeResponseDTO>().ReverseMap();
            CreateMap<Skill, SkillRequestDTO>().ReverseMap();
        }
    }
}
