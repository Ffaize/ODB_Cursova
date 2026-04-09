using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Reporting
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class TransactionHistoryController(ILogger<TransactionHistoryController> logger, TransactionHistoryService transactionHistoryService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var history = await transactionHistoryService.GetAll();
                return Ok(history);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all transaction history.");
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
                    return BadRequest("Customer ID must be a valid GUID.");
                }
                var history = await transactionHistoryService.GetByCustomerId(customerId);
                return Ok(history);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching transaction history for customer {CustomerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByDateRange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate == default || endDate == default)
                {
                    return BadRequest("Both startDate and endDate parameters are required.");
                }
                if (startDate > endDate)
                {
                    return BadRequest("Start date must be less than or equal to end date.");
                }
                var history = await transactionHistoryService.GetByDateRange(startDate, endDate);
                return Ok(history);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching transaction history for date range {StartDate} to {EndDate}.", startDate, endDate);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetLargeTransactions")]
        public async Task<IActionResult> GetLargeTransactions([FromQuery] decimal minimumAmount = 0)
        {
            try
            {
                if (minimumAmount < 0)
                {
                    return BadRequest("Minimum amount must be greater than or equal to 0.");
                }
                var history = await transactionHistoryService.GetLargeTransactions(minimumAmount);
                return Ok(history);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching large transactions with minimum amount {MinimumAmount}.", minimumAmount);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
