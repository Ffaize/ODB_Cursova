using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;
using WebAPI.Entities.DTOs;

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

        [HttpPost("create-with-account")]
        public async Task<IActionResult> CreateCustomerWithAccount([FromBody] CreateCustomerWithAccountRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is required.");
                }

                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Surname) ||
                    string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest("Name, Surname, Email, and Password are required fields.");
                }

                var result = await customerService.CreateCustomerWithAccount(request);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create customer with account. Email may already exist.");
                }

                return CreatedAtAction(nameof(GetCustomerById), new { id = result.CustomerId }, result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while creating customer with account.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{customerId}/accounts")]
        public async Task<IActionResult> GetCustomerAccounts(Guid customerId)
        {
            try
            {
                if (customerId == Guid.Empty)
                {
                    return BadRequest("Valid CustomerId is required.");
                }

                var result = await customerService.GetCustomerAccountsWithBalance(customerId);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve customer accounts.");
                }

                if (result.Accounts.Count == 0)
                {
                    return NotFound($"No accounts found for customer with ID {customerId}.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while retrieving customer accounts for customer with ID {CustomerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
