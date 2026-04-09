using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Analytics
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class LoanRiskController(ILogger<LoanRiskController> logger, LoanRiskService loanRiskService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await loanRiskService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all loan risk data.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByRiskLevel")]
        public async Task<IActionResult> GetByRiskLevel([FromQuery] string riskLevel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(riskLevel))
                {
                    return BadRequest("Risk level cannot be empty.");
                }

                var results = await loanRiskService.GetByRiskLevel(riskLevel);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching loan risk data by risk level {RiskLevel}.", riskLevel);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetHighRisk")]
        public async Task<IActionResult> GetHighRisk()
        {
            try
            {
                var results = await loanRiskService.GetHighRisk();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching high risk loan data.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
