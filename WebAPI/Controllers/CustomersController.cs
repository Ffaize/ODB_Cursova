using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;

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
    }
}
