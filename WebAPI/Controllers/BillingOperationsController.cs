using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingOperationsController(ILogger<BillingOperationsController> logger, BillingOperationService billingOperationService) : ControllerBase
    {
        [HttpGet("GetAllBillingOperations")]
        public async Task<IActionResult> GetAllBillingOperations()
        {
            try
            {
                var billingOperations = await billingOperationService.GetAllBillingOperations();
                return Ok(billingOperations);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all billing operations.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillingOperationById(Guid id)
        {
            try
            {
                var billingOperation = await billingOperationService.GetBillingOperationById(id);
                if (billingOperation == null)
                {
                    return NotFound($"Billing operation with ID {id} not found.");
                }
                return Ok(billingOperation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching billing operation with ID {BillingOperationId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBillingOperation([FromBody] BillingOperation billingOperation)
        {
            try
            {
                if (billingOperation == null)
                {
                    return BadRequest("Billing operation data is required.");
                }
                var success = await billingOperationService.AddBillingOperation(billingOperation);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add billing operation.") : 
                    CreatedAtAction(nameof(GetBillingOperationById), new { id = billingOperation.Id }, billingOperation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new billing operation.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillingOperation(Guid id, [FromBody] BillingOperation billingOperation)
        {
            try
            {
                if (billingOperation == null)
                {
                    return BadRequest("Billing operation data is required.");
                }
                if (billingOperation.Id != id)
                {
                    return BadRequest("Billing operation ID in URL does not match billing operation data.");
                }
                var success = await billingOperationService.UpdateBillingOperation(billingOperation);
                return !success ? 
                    NotFound($"Billing operation with ID {id} not found or update failed.") : 
                    Ok(billingOperation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating billing operation with ID {BillingOperationId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingOperation(Guid id)
        {
            try
            {
                var success = await billingOperationService.DeleteBillingOperation(id);
                return !success ? 
                    NotFound($"Billing operation with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting billing operation with ID {BillingOperationId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockBillingOperations([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await billingOperationService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock billing operations.") : 
                    Ok($"Successfully generated {count} mock billing operation(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock billing operations.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
