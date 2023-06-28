using FirstAngularAPIAssignment.Constants;
using FirstAngularAPIAssignment.DTO;
using FirstAngularAPIAssignment.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAngularAPIAssignment.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _iEmployeeRepository;

        public EmployeeController(IEmployeeRepository iEmployeeRepository)
        {
            this._iEmployeeRepository = iEmployeeRepository;
        }

        /// <summary>
        /// Get All Employee Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employee = await _iEmployeeRepository.GetAllEmployees();
                if (employee != null)
                {
                    return Ok(employee);
                }
                return NotFound(Constant.RecordNotFound);

            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

        [HttpPost]        
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRequestDTO addEmployeeRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var empRecord = await _iEmployeeRepository.CheckEmailExistsInEmployee(addEmployeeRequest.Email);
                if (empRecord != null)
                {
                    return Conflict(Constant.TheRecordAlreadyExists);
                }

                var result = await _iEmployeeRepository.AddEmployee(addEmployeeRequest);

                return Created($"/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

        [HttpGet("{empId}")]
        public async Task<IActionResult> GetEmployeeById(int empId)
        {
            try
            {
                if (empId == 0) return BadRequest(Constant.EnterEmployeeId);

                var employee = await _iEmployeeRepository.GetndCheckEmployeesById(empId);
                if (employee == null)
                {
                    return NotFound(Constant.TheKeyDoesNotExist);
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }


        [HttpPut("{empId}")]
        public async Task<IActionResult> UpdateEmployee(int empId, [FromBody] EmployeeRequestDTO updateEmployeeRequest)
        {
            try
            {
                if (empId == 0) return BadRequest(Constant.EnterEmployeeId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = await _iEmployeeRepository.GetndCheckEmployeesById(empId);

                if (employee != null)
                {
                    var result = await _iEmployeeRepository.UpdateEmployee(employee, updateEmployeeRequest);
                    return Ok(result);
                }
                return NotFound(Constant.TheKeyDoesNotExist);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

      
        [HttpDelete("{empId}")]
        public async Task<IActionResult> DeleteEmployee(int empId)
        {
            try
            {
                if (empId == 0) return BadRequest(Constant.EnterEmployeeId);

                var employee = await _iEmployeeRepository.GetndCheckEmployeesById(empId);
                if (employee != null)
                {
                    _iEmployeeRepository.DeleteEmployee(employee);
                    return Ok();

                }
                return NotFound(Constant.TheKeyDoesNotExist);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

    }
}
