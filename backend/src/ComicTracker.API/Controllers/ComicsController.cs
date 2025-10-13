using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using comicTracker.DTOs;
using comicTracker.Services;

namespace comicTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComicsController : ControllerBase
    {
        private readonly IComicService _comicService;
        private readonly ILogger<ComicsController> _logger;

        public ComicsController(IComicService comicService, ILogger<ComicsController> logger)
        {
            _comicService = comicService;
            _logger = logger;
        }

        /// <summary>
        /// Get user's comic collection with pagination and filtering
        /// </summary>
        /// <param name="query">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of comics</returns>
        [HttpGet]
        public async Task<IActionResult> GetComics([FromQuery] ComicQuery query)
        {
            try
            {
                var userId = GetCurrentUserId();
                var comics = await _comicService.GetUserComicsAsync(userId, query);
                
                return Ok(ApiResponse<PagedResult<ComicDto>>.SuccessResponse(comics, "Comics retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comics for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get a specific comic by ID
        /// </summary>
        /// <param name="id">Comic ID</param>
        /// <returns>Comic details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComic(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var comic = await _comicService.GetComicByIdAsync(id, userId);
                
                if (comic == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Comic not found"));
                }

                return Ok(ApiResponse<ComicDto>.SuccessResponse(comic, "Comic retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comic {ComicId} for user {UserId}", id, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Create a new comic
        /// </summary>
        /// <param name="request">Comic creation details</param>
        /// <returns>Created comic</returns>
        [HttpPost]
        public async Task<IActionResult> CreateComic([FromBody] CreateComicRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var userId = GetCurrentUserId();
                var comic = await _comicService.CreateComicAsync(request, userId);

                return CreatedAtAction(nameof(GetComic), new { id = comic.Id }, 
                    ApiResponse<ComicDto>.SuccessResponse(comic, "Comic created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating comic for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Update an existing comic
        /// </summary>
        /// <param name="id">Comic ID</param>
        /// <param name="request">Updated comic details</param>
        /// <returns>Updated comic</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComic(int id, [FromBody] UpdateComicRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var userId = GetCurrentUserId();
                var comic = await _comicService.UpdateComicAsync(id, request, userId);

                if (comic == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Comic not found"));
                }

                return Ok(ApiResponse<ComicDto>.SuccessResponse(comic, "Comic updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comic {ComicId} for user {UserId}", id, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Delete a comic
        /// </summary>
        /// <param name="id">Comic ID</param>
        /// <returns>Success message</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _comicService.DeleteComicAsync(id, userId);

                if (!success)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Comic not found"));
                }

                return Ok(ApiResponse<string>.SuccessResponse("Comic deleted successfully", "Comic deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comic {ComicId} for user {UserId}", id, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Search comics by series name, issue number, or publisher
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <returns>List of matching comics</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchComics([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Search term is required"));
                }

                var userId = GetCurrentUserId();
                var comics = await _comicService.SearchComicsAsync(searchTerm, userId);

                return Ok(ApiResponse<List<ComicDto>>.SuccessResponse(comics, "Search completed successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching comics for user {UserId} with term {SearchTerm}", 
                    GetCurrentUserId(), searchTerm);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user token");
            }
            return userId;
        }
    }
}