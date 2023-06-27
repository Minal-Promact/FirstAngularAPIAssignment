using AutoMapper;
using FirstAngularAPIAssignment.Data;
using FirstAngularAPIAssignment.DTO;
using FirstAngularAPIAssignment.Model;
using FirstAngularAPIAssignment.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace FirstAngularAPIAssignment.Repository.Implementation
{
    public class EmployeeRepository :IEmployeeRepository
    {
        private readonly EFDataContext dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(EFDataContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponseDTO>> GetAllEmployees()
        {           
            List<Employee> employees = await dbContext.Employees.Include(a=>a.Skills).ToListAsync();
            List<EmployeeResponseDTO> lstEmployeeResponseDTO = employees.Select(a => new EmployeeResponseDTO() 
            {
                Id = a.Id,
                Name = a.Name,
                ContactNumber = a.ContactNumber,
                Email = a.Email,
                Gender = a.Gender
            }).ToList();

            lstEmployeeResponseDTO.ForEach(e =>
            {
                int i = 1;
                List<Skill> skills = dbContext.Skills.Where(a => a.EmployeeId == e.Id).ToList();
                e.Skills = new List<string>();
                
                skills.ForEach(y =>
                {
                    string val = string.Empty;
                    if (skills.Count() > 1)
                    {
                        val = (i) + ". " + y.SkillName + " (Exp. " + y.SkillExperience + " years)";
                    }
                    else if (skills.Count() == 1)
                    {
                        val = y.SkillName + " (Exp. " + y.SkillExperience + " years)";
                    }
                    e.Skills.Add(val);
                    i++;
                });

            });

            return lstEmployeeResponseDTO;
        }

        public async Task<Employee> GetndCheckEmployeesById(int empId)
        {
            var employee = await dbContext.Employees.Include(_ => _.Skills).Where(e => e.Id == empId).FirstOrDefaultAsync();
            return employee;
        }

        public async Task<Employee> CheckEmailExistsInEmployee(string Email)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(a => a.Email == Email);
        }

        public async Task<Employee> AddEmployee(EmployeeRequestDTO addEmployeeRequest)
        {
            var employee = _mapper.Map<EmployeeRequestDTO, Employee>(addEmployeeRequest);
            await dbContext.Employees.AddAsync(employee);

            //for(int i = 0; i < addEmployeeRequest.Skills.Count; i++)
            //{
            //    Skill skill = new Skill();
            //    skill.EmployeeId = addEmployeeRequest.Id;
            //    skill.SkillName = addEmployeeRequest.Skills[i].SkillName;
            //    skill.SkillExperience = addEmployeeRequest.Skills[i].SkillExperience;

            //    await dbContext.Skills.AddAsync(skill);
            //}
            //addEmployeeRequest.Skills.ForEach(e => {               
                
            //});           

            await dbContext.SaveChangesAsync();            

            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee emp, EmployeeRequestDTO updateEmployeeRequest)
        {
            emp.Name = updateEmployeeRequest.Name;
            emp.Gender = updateEmployeeRequest.Gender;
            emp.Email = updateEmployeeRequest.Email;
            emp.ContactNumber = updateEmployeeRequest.ContactNumber;
            
            foreach (var empskills in emp.Skills.ToList())
            {
                emp.Skills.Remove(empskills);
            }

            updateEmployeeRequest.Skills.ForEach(e =>
            {
                var result = emp.Skills.FirstOrDefault(x => x.SkillName == e.SkillName);
                if (result == null)
                {
                    Skill skill = new Skill();
                    skill.EmployeeId = updateEmployeeRequest.Id;
                    skill.SkillName = e.SkillName;
                    skill.SkillExperience = e.SkillExperience;
                    emp.Skills.Add(skill);                    
                }
                else
                {
                    emp.Skills.FirstOrDefault(x => x.SkillName == e.SkillName).SkillName = e.SkillName;
                    emp.Skills.FirstOrDefault(x => x.SkillName == e.SkillName).SkillExperience = e.SkillExperience;                    
                    
                }
            });

            dbContext.Employees.Update(emp);
            await dbContext.SaveChangesAsync();

            var empl = await GetndCheckEmployeesById(updateEmployeeRequest.Id);

            return empl;
        }

        public void DeleteEmployee(Employee emp)
        {
            dbContext.Employees.Remove(emp); 
            dbContext.SaveChangesAsync();
        }
    }
}
