using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;
using WebAPI.Entities.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditsController(ILogger<CreditsController> logger, CreditService creditService) : ControllerBase
    {
        [HttpGet("GetAllCredits")]
        public async Task<IActionResult> GetAllCredits()
        {
            try
            {
                var credits = await creditService.GetAllCredits();
                return Ok(credits);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all credits.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditById(Guid id)
        {
            try
            {
                var credit = await creditService.GetCreditById(id);
                if (credit == null)
                {
                    return NotFound($"Credit with ID {id} not found.");
                }
                return Ok(credit);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching credit with ID {CreditId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCredit([FromBody] Credit credit)
        {
            try
            {
                if (credit == null)
                {
                    return BadRequest("Credit data is required.");
                }
                var success = await creditService.AddCredit(credit);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add credit.") : 
                    CreatedAtAction(nameof(GetCreditById), new { id = credit.Id }, credit);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new credit.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCredit(Guid id, [FromBody] Credit credit)
        {
            try
            {
                if (credit == null)
                {
                    return BadRequest("Credit data is required.");
                }
                if (credit.Id != id)
                {
                    return BadRequest("Credit ID in URL does not match credit data.");
                }
                var success = await creditService.UpdateCredit(credit);
                return !success ? 
                    NotFound($"Credit with ID {id} not found or update failed.") : 
                    Ok(credit);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating credit with ID {CreditId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredit(Guid id)
        {
            try
            {
                var success = await creditService.DeleteCredit(id);
                return !success ? 
                    NotFound($"Credit with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting credit with ID {CreditId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockCredits([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await creditService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock credits.") : 
                    Ok($"Successfully generated {count} mock credit(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock credits.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended")]
        public async Task<IActionResult> GetExtendedAllCredits()
        {
            try
            {
                var items = await creditService.GetExtendedAllCredits();
                return Ok(items);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended credits.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended/{id}")]
        public async Task<IActionResult> GetExtendedCreditById(Guid id)
        {
            try
            {
                var item = await creditService.GetExtendedCreditById(id);
                if (item == null)
                {
                    return NotFound($"Credit with ID {id} not found.");
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended credit with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenCredit([FromBody] OpenCreditRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is required.");
                }

                if (request.CustomerId == Guid.Empty)
                {
                    return BadRequest("Valid CustomerId is required.");
                }

                if (request.FullAmount <= 0)
                {
                    return BadRequest("Full amount must be greater than 0.");
                }

                if (request.DurationInMonths <= 0)
                {
                    return BadRequest("Duration in months must be greater than 0.");
                }

                var result = await creditService.OpenCredit(request);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to open credit.");
                }

                return CreatedAtAction(nameof(GetCreditById), new { id = result.CreditId }, result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while opening credit.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{creditId}/schedule")]
        public async Task<IActionResult> GetPaymentSchedule(Guid creditId)
        {
            try
            {
                if (creditId == Guid.Empty)
                {
                    return BadRequest("Valid CreditId is required.");
                }

                var schedule = await creditService.GetPaymentSchedule(creditId);
                
                if (schedule == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve payment schedule.");
                }

                if (schedule.Count == 0)
                {
                    return NotFound($"No payment schedule found for credit with ID {creditId}.");
                }

                return Ok(schedule);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while retrieving payment schedule for credit with ID {CreditId}.", creditId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{creditId}/close")]
        public async Task<IActionResult> CloseCredit(Guid creditId)
        {
            try
            {
                if (creditId == Guid.Empty)
                {
                    return BadRequest("Valid CreditId is required.");
                }

                var success = await creditService.CloseCredit(creditId);
                
                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to close credit.");
                }

                return Ok("Credit closed successfully.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while closing credit with ID {CreditId}.", creditId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
