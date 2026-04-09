using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionLogsController(ILogger<ActionLogsController> logger, ActionLogService actionLogService) : ControllerBase
    {
        [HttpGet("GetAllActionLogs")]
        public async Task<IActionResult> GetAllActionLogs()
        {
            try
            {
                var actionLogs = await actionLogService.GetAllActionLogs();
                return Ok(actionLogs);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all action logs.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActionLogById(Guid id)
        {
            try
            {
                var actionLog = await actionLogService.GetActionLogById(id);
                if (actionLog == null)
                {
                    return NotFound($"Action log with ID {id} not found.");
                }
                return Ok(actionLog);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching action log with ID {ActionLogId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddActionLog([FromBody] ActionLog actionLog)
        {
            try
            {
                if (actionLog == null)
                {
                    return BadRequest("Action log data is required.");
                }
                var success = await actionLogService.AddActionLog(actionLog);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add action log.") : 
                    CreatedAtAction(nameof(GetActionLogById), new { id = actionLog.Id }, actionLog);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new action log.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActionLog(Guid id, [FromBody] ActionLog actionLog)
        {
            try
            {
                if (actionLog == null)
                {
                    return BadRequest("Action log data is required.");
                }
                if (actionLog.Id != id)
                {
                    return BadRequest("Action log ID in URL does not match action log data.");
                }
                var success = await actionLogService.UpdateActionLog(actionLog);
                return !success ? 
                    NotFound($"Action log with ID {id} not found or update failed.") : 
                    Ok(actionLog);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating action log with ID {ActionLogId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActionLog(Guid id)
        {
            try
            {
                var success = await actionLogService.DeleteActionLog(id);
                return !success ? 
                    NotFound($"Action log with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting action log with ID {ActionLogId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockActionLogs([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await actionLogService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock action logs.") : 
                    Ok($"Successfully generated {count} mock action log(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock action logs.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
