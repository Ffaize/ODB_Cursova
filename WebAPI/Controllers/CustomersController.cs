using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(ILogger<CustomersController> logger, CustomerService customerService) : ControllerBase
    {
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all customers.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found.");
                }
                return Ok(customer);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching customer with ID {CustomerId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is required.");
                }
                var success = await customerService.AddCustomer(customer);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add customer.") : 
                    CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new customer.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is required.");
                }
                if (customer.Id != id)
                {
                    return BadRequest("Customer ID in URL does not match customer data.");
                }
                var success = await customerService.UpdateCustomer(customer);
                return !success ? 
                    NotFound($"Customer with ID {id} not found or update failed.") : 
                    Ok(customer);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating customer with ID {CustomerId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var success = await customerService.DeleteCustomer(id);
                return !success ? 
                    NotFound($"Customer with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting customer with ID {CustomerId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockCustomers([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await customerService.AddMockCustomers(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock customers.") : 
                    Ok($"Successfully generated {count} mock customer(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock customers.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
