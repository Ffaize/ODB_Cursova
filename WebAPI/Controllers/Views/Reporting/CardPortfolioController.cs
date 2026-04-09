using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Reporting
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class CardPortfolioController(ILogger<CardPortfolioController> logger, CardPortfolioService cardPortfolioService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var portfolios = await cardPortfolioService.GetAll();
                return Ok(portfolios);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all card portfolios.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByStatus")]
        public async Task<IActionResult> GetByStatus([FromQuery] string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    return BadRequest("Status parameter is required.");
                }
                var portfolios = await cardPortfolioService.GetByStatus(status);
                return Ok(portfolios);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching card portfolios by status {Status}.", status);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetExpiringSoon")]
        public async Task<IActionResult> GetExpiringSoon([FromQuery] int daysUntilExpiry = 90)
        {
            try
            {
                if (daysUntilExpiry < 0)
                {
                    return BadRequest("Days until expiry must be greater than or equal to 0.");
                }
                var portfolios = await cardPortfolioService.GetExpiringSoon(daysUntilExpiry);
                return Ok(portfolios);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching expiring cards with days until expiry {DaysUntilExpiry}.", daysUntilExpiry);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
