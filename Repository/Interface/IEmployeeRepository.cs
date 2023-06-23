using FirstAngularAPIAssignment.DTO;
using FirstAngularAPIAssignment.Model;

namespace FirstAngularAPIAssignment.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeResponseDTO>> GetAllEmployees();
        Task<Employee> GetndCheckEmployeesById(int empId);
        Task<Employee> CheckEmailExistsInEmployee(string Email);
        Task<Employee> AddEmployee(EmployeeRequestDTO addEmployeeRequest);
        Task<Employee> UpdateEmployee(Employee emp, EmployeeRequestDTO updateEmployeeRequest);
        void DeleteEmployee(Employee emp);
    }
}
