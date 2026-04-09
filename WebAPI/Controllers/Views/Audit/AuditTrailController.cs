using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices.Views;

namespace WebAPI.Controllers.Views.Audit
{
    [ApiController]
    [Route("api/views/[controller]")]
    public class AuditTrailController(ILogger<AuditTrailController> logger, AuditTrailService auditTrailService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await auditTrailService.GetAll();
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all audit trail records.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetByOperationType")]
        public async Task<IActionResult> GetByOperationType([FromQuery] string operationType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(operationType))
                {
                    return BadRequest("Operation type parameter is required.");
                }
                var results = await auditTrailService.GetByOperationType(operationType);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching audit trail records by operation type: {OperationType}.", operationType);
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
                    return BadRequest("Start date cannot be after end date.");
                }
                var results = await auditTrailService.GetByDateRange(startDate, endDate);
                return Ok(results);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching audit trail records by date range: {StartDate} to {EndDate}.", startDate, endDate);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
