using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Operations
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class BalanceDailyController(ILogger<BalanceDailyController> logger, BalanceDailyService balanceDailyService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await balanceDailyService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all balance daily records.");
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
                    return BadRequest("Customer ID is required and must be a valid GUID.");
                }
                var results = await balanceDailyService.GetByCustomerId(customerId);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching balance daily records for customer {CustomerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetLowBalance")]
        public async Task<IActionResult> GetLowBalance([FromQuery] decimal thresholdBalance = 1000)
        {
            try
            {
                if (thresholdBalance < 0)
                {
                    return BadRequest("Threshold balance must be a non-negative value.");
                }
                var results = await balanceDailyService.GetLowBalance(thresholdBalance);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching low balance records with threshold {ThresholdBalance}.", thresholdBalance);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
