using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Reporting
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class EmployeeBranchStatsController(ILogger<EmployeeBranchStatsController> logger, EmployeeBranchStatsService employeeBranchStatsService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var stats = await employeeBranchStatsService.GetAll();
                return Ok(stats);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all employee branch stats.");
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
                    return BadRequest("Branch ID must be a valid GUID.");
                }
                var stats = await employeeBranchStatsService.GetByBranchId(branchId);
                return Ok(stats);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching employee branch stats for branch {BranchId}.", branchId);
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
                var stats = await employeeBranchStatsService.GetTopPerformers(topCount);
                return Ok(stats);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching top {TopCount} employee performers.", topCount);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
