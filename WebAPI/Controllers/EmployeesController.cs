using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(ILogger<EmployeesController> logger, EmployeeService employeeService) : ControllerBase
    {
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await employeeService.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all employees.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
                return Ok(employee);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching employee with ID {EmployeeId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee data is required.");
                }
                var success = await employeeService.AddEmployee(employee);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add employee.") : 
                    CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new employee.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee data is required.");
                }
                if (employee.Id != id)
                {
                    return BadRequest("Employee ID in URL does not match employee data.");
                }
                var success = await employeeService.UpdateEmployee(employee);
                return !success ? 
                    NotFound($"Employee with ID {id} not found or update failed.") : 
                    Ok(employee);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating employee with ID {EmployeeId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var success = await employeeService.DeleteEmployee(id);
                return !success ? 
                    NotFound($"Employee with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting employee with ID {EmployeeId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockEmployees([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await employeeService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock employees.") : 
                    Ok($"Successfully generated {count} mock employee(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock employees.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
