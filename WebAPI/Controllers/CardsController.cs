using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

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
    }
}
