using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;
using WebAPI.Entities.DTOs;

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

        [HttpGet("extended")]
        public async Task<IActionResult> GetExtendedAllBillingOperations()
        {
            try
            {
                var items = await billingOperationService.GetExtendedAllBillingOperations();
                return Ok(items);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended billing operations.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended/{id}")]
        public async Task<IActionResult> GetExtendedBillingOperationById(Guid id)
        {
            try
            {
                var item = await billingOperationService.GetExtendedBillingOperationById(id);
                if (item == null)
                {
                    return NotFound($"Billing operation with ID {id} not found.");
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended billing operation with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{billingNumberId}/balance")]
        public async Task<IActionResult> CheckBalance(Guid billingNumberId)
        {
            try
            {
                if (billingNumberId == Guid.Empty)
                {
                    return BadRequest("Valid BillingNumberId is required.");
                }

                var result = await billingOperationService.CheckBalance(billingNumberId);
                
                if (result == null)
                {
                    return NotFound($"Account with ID {billingNumberId} not found.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while checking balance for account {BillingNumberId}.", billingNumberId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("transfer-between")]
        public async Task<IActionResult> TransferBetweenAccounts([FromBody] TransferBetweenAccountsRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is required.");
                }

                if (request.FromBillingNumberId == Guid.Empty || request.ToBillingNumberId == Guid.Empty)
                {
                    return BadRequest("Valid source and destination account IDs are required.");
                }

                if (request.Amount <= 0)
                {
                    return BadRequest("Amount must be greater than 0.");
                }

                var result = await billingOperationService.TransferBetweenAccounts(request);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to transfer between accounts.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while transferring between accounts.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("transfer-to-other")]
        public async Task<IActionResult> TransferToOtherCustomer([FromBody] TransferToOtherCustomerRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is required.");
                }

                if (request.FromBillingNumberId == Guid.Empty)
                {
                    return BadRequest("Valid source account ID is required.");
                }

                if (string.IsNullOrWhiteSpace(request.ToAccountNumber))
                {
                    return BadRequest("Destination account number is required.");
                }

                if (request.Amount <= 0)
                {
                    return BadRequest("Amount must be greater than 0.");
                }

                if (string.IsNullOrWhiteSpace(request.Description))
                {
                    return BadRequest("Transfer description is required.");
                }

                var result = await billingOperationService.TransferToOtherCustomer(request);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to transfer to other customer.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while transferring to other customer.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("pay")]
        public async Task<IActionResult> PayBilling([FromBody] PayBillingRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is required.");
                }

                if (request.BillingNumberId == Guid.Empty)
                {
                    return BadRequest("Valid BillingNumberId is required.");
                }

                if (request.Amount <= 0)
                {
                    return BadRequest("Amount must be greater than 0.");
                }

                var result = await billingOperationService.PayBilling(request);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to process payment.");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing billing payment.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
