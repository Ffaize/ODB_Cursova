using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Analytics
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class CreditPortfolioController(ILogger<CreditPortfolioController> logger, CreditPortfolioService creditPortfolioService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await creditPortfolioService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all credit portfolio data.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByCreditStatus")]
        public async Task<IActionResult> GetByCreditStatus([FromQuery] string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Credit status cannot be empty.");
                }

                var results = await creditPortfolioService.GetByCreditStatus(status);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching credit portfolio data by status {Status}.", status);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetOverdue")]
        public async Task<IActionResult> GetOverdue()
        {
            try
            {
                var results = await creditPortfolioService.GetOverdue();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching overdue credit portfolio data.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByCurrency")]
        public async Task<IActionResult> GetByCurrency([FromQuery] string currency)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currency))
                {
                    return BadRequest("Currency cannot be empty.");
                }

                var results = await creditPortfolioService.GetByCurrency(currency);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching credit portfolio data by currency {Currency}.", currency);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
