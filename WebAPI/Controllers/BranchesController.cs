using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataServices;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController(ILogger<BranchesController> logger, BranchService branchService) : ControllerBase
    {
        [HttpGet("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches()
        {
            try
            {
                var branches = await branchService.GetAllBranches();
                return Ok(branches);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching all branches.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(Guid id)
        {
            try
            {
                var branch = await branchService.GetBranchById(id);
                if (branch == null)
                {
                    return NotFound($"Branch with ID {id} not found.");
                }
                return Ok(branch);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while fetching branch with ID {BranchId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch([FromBody] Branch branch)
        {
            try
            {
                if (branch == null)
                {
                    return BadRequest("Branch data is required.");
                }
                var success = await branchService.AddBranch(branch);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to add branch.") : 
                    CreatedAtAction(nameof(GetBranchById), new { id = branch.Id }, branch);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new branch.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(Guid id, [FromBody] Branch branch)
        {
            try
            {
                if (branch == null)
                {
                    return BadRequest("Branch data is required.");
                }
                if (branch.Id != id)
                {
                    return BadRequest("Branch ID in URL does not match branch data.");
                }
                var success = await branchService.UpdateBranch(branch);
                return !success ? 
                    NotFound($"Branch with ID {id} not found or update failed.") : 
                    Ok(branch);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating branch with ID {BranchId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            try
            {
                var success = await branchService.DeleteBranch(id);
                return !success ? 
                    NotFound($"Branch with ID {id} not found or delete failed.") : 
                    NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting branch with ID {BranchId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AddMockBranches([FromQuery] int count = 1)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0.");
                }
                var success = await branchService.AddMockData(count);
                return !success ? 
                    StatusCode(StatusCodes.Status500InternalServerError, "Failed to generate mock branches.") : 
                    Ok($"Successfully generated {count} mock branch(es).");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while generating mock branches.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
