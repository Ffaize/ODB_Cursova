using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Audit
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class AnomalyController(ILogger<AnomalyController> logger, AnomalyService anomalyService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await anomalyService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all anomaly records.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetRecent")]
        public async Task<IActionResult> GetRecent([FromQuery] int hoursBack = 24)
        {
            try
            {
                if (hoursBack <= 0)
                {
                    return BadRequest("Hours back parameter must be greater than 0.");
                }
                var results = await anomalyService.GetRecent(hoursBack);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching recent anomaly records for the last {HoursBack} hours.", hoursBack);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetBySeverity")]
        public async Task<IActionResult> GetBySeverity([FromQuery] string severity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(severity))
                {
                    return BadRequest("Severity parameter is required.");
                }
                var results = await anomalyService.GetBySeverity(severity);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching anomaly records by severity: {Severity}.", severity);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
