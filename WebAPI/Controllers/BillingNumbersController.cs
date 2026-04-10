using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingNumbersController(ILogger<BillingNumbersController> logger, BillingNumberService billingNumberService) : ControllerBase
    {
        [HttpGet("GetAllBillingNumbers")]
        public async Task<IActionResult> GetAllBillingNumbers()
        {
            try
            {
                var billingNumbers = await billingNumberService.GetAllBillingNumbers();
                return Ok(billingNumbers);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all billing numbers.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillingNumberById(Guid id)
        {
            try
            {
                var billingNumber = await billingNumberService.GetBillingNumberById(id);
                if (billingNumber == null)
                {
                    return NotFound($"Billing number with ID {id} not found.");
                }
                return Ok(billingNumber);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching billing number with ID {BillingNumberId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBillingNumber([FromBody] BillingNumber billingNumber)
        {
            try
            {
                if (billingNumber == null)
                {
                    return BadRequest("Billing number data is required.");
                }
                var success = await billingNumberService.AddBillingNumber(billingNumber);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add billing number.") : 
                    CreatedAtAction(nameof(GetBillingNumberById), new { id = billingNumber.Id }, billingNumber);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new billing number.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillingNumber(Guid id, [FromBody] BillingNumber billingNumber)
        {
            try
            {
                if (billingNumber == null)
                {
                    return BadRequest("Billing number data is required.");
                }
                if (billingNumber.Id != id)
                {
                    return BadRequest("Billing number ID in URL does not match billing number data.");
                }
                var success = await billingNumberService.UpdateBillingNumber(billingNumber);
                return !success ? 
                    NotFound($"Billing number with ID {id} not found or update failed.") : 
                    Ok(billingNumber);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating billing number with ID {BillingNumberId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingNumber(Guid id)
        {
            try
            {
                var success = await billingNumberService.DeleteBillingNumber(id);
                return !success ? 
                    NotFound($"Billing number with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting billing number with ID {BillingNumberId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockBillingNumbers([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await billingNumberService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock billing numbers.") : 
                    Ok($"Successfully generated {count} mock billing number(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock billing numbers.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended")]
        public async Task<IActionResult> GetExtendedAllBillingNumbers()
        {
            try
            {
                var items = await billingNumberService.GetExtendedAllBillingNumbers();
                return Ok(items);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended billing numbers.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended/{id}")]
        public async Task<IActionResult> GetExtendedBillingNumberById(Guid id)
        {
            try
            {
                var item = await billingNumberService.GetExtendedBillingNumberById(id);
                if (item == null)
                {
                    return NotFound($"Billing number with ID {id} not found.");
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended billing number with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
