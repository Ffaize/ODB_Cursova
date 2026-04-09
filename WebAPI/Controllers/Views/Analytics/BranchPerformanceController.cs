using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Analytics
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class BranchPerformanceController(ILogger<BranchPerformanceController> logger, BranchPerformanceService branchPerformanceService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await branchPerformanceService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all branch performance data.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByBranchId")]
        public async Task<IActionResult> GetByBranchId([FromQuery] Guid branchId)
        {
            try
            {
                if (branchId == Guid.Empty)
                {
                    return BadRequest("Branch ID cannot be empty.");
                }

                var result = await branchPerformanceService.GetByBranchId(branchId);
                if (result == null)
                {
                    return NotFound($"No branch performance data found for branch ID {branchId}.");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching branch performance data for branch ID {BranchId}.", branchId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetTopPerformers")]
        public async Task<IActionResult> GetTopPerformers([FromQuery] int topCount = 10)
        {
            try
            {
                if (topCount <= 0)
                {
                    return BadRequest("Top count must be greater than 0.");
                }

                var results = await branchPerformanceService.GetTopPerformers(topCount);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching top {TopCount} performing branches.", topCount);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
