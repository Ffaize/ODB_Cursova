using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Analytics
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class CustomerAccountsController(ILogger<CustomerAccountsController> logger, CustomerAccountsService customerAccountsService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await customerAccountsService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all customer accounts data.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId([FromQuery] Guid customerId)
        {
            try
            {
                if (customerId == Guid.Empty)
                {
                    return BadRequest("Customer ID cannot be empty.");
                }

                var result = await customerAccountsService.GetByCustomerId(customerId);
                if (result == null)
                {
                    return NotFound($"No customer accounts data found for customer ID {customerId}.");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching customer accounts data for customer ID {CustomerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetHighBalance")]
        public async Task<IActionResult> GetHighBalance([FromQuery] decimal minimumBalance = 10000)
        {
            try
            {
                if (minimumBalance < 0)
                {
                    return BadRequest("Minimum balance cannot be negative.");
                }

                var results = await customerAccountsService.GetHighBalance(minimumBalance);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching high balance customer accounts with minimum balance {MinimumBalance}.", minimumBalance);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
