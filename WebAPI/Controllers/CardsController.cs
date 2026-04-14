using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;
using WebAPI.Entities.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController(ILogger<CardsController> logger, CardService cardService) : ControllerBase
    {
        [HttpGet("GetAllCards")]
        public async Task<IActionResult> GetAllCards()
        {
            try
            {
                var cards = await cardService.GetAllCards();
                return Ok(cards);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all cards.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(Guid id)
        {
            try
            {
                var card = await cardService.GetCardById(id);
                if (card == null)
                {
                    return NotFound($"Card with ID {id} not found.");
                }
                return Ok(card);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching card with ID {CardId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            try
            {
                if (card == null)
                {
                    return BadRequest("Card data is required.");
                }
                var success = await cardService.AddCard(card);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add card.") : 
                    CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new card.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(Guid id, [FromBody] Card card)
        {
            try
            {
                if (card == null)
                {
                    return BadRequest("Card data is required.");
                }
                if (card.Id != id)
                {
                    return BadRequest("Card ID in URL does not match card data.");
                }
                var success = await cardService.UpdateCard(card);
                return !success ? 
                    NotFound($"Card with ID {id} not found or update failed.") : 
                    Ok(card);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating card with ID {CardId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            try
            {
                var success = await cardService.DeleteCard(id);
                return !success ? 
                    NotFound($"Card with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting card with ID {CardId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockCards([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await cardService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock cards.") : 
                    Ok($"Successfully generated {count} mock card(s).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock cards.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended")]
        public async Task<IActionResult> GetExtendedAllCards()
        {
            try
            {
                var items = await cardService.GetExtendedAllCards();
                return Ok(items);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended cards.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("extended/{id}")]
        public async Task<IActionResult> GetExtendedCardById(Guid id)
        {
            try
            {
                var item = await cardService.GetExtendedCardById(id);
                if (item == null)
                {
                    return NotFound($"Card with ID {id} not found.");
                }
                return Ok(item);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching extended card with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("issue")]
        public async Task<IActionResult> IssueCard([FromBody] IssueCardRequest request)
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

                if (request.CardType <= 0)
                {
                    return BadRequest("Valid CardType is required.");
                }

                var result = await cardService.IssueCard(request);
                
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to issue card.");
                }

                return CreatedAtAction(nameof(GetCardById), new { id = result.CardId }, result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while issuing card.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{cardId}/block")]
        public async Task<IActionResult> BlockCard(Guid cardId)
        {
            try
            {
                if (cardId == Guid.Empty)
                {
                    return BadRequest("Valid CardId is required.");
                }

                var success = await cardService.BlockCard(cardId);
                
                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to block card.");
                }

                return Ok("Card blocked successfully.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while blocking card with ID {CardId}.", cardId);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
