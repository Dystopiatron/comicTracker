using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using comicTracker.DTOs;
using comicTracker.Services;

namespace comicTracker.Controllers
{
    /// <summary>
    /// Controller for ComicVine API integration - search and import comics from the ComicVine database
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ComicVineController : ControllerBase
    {
        private readonly IComicVineService _comicVineService;
        private readonly ILogger<ComicVineController> _logger;

        public ComicVineController(IComicVineService comicVineService, ILogger<ComicVineController> logger)
        {
            _comicVineService = comicVineService;
            _logger = logger;
        }

        /// <summary>
        /// Check if ComicVine API integration is configured and available
        /// </summary>
        /// <returns>Status indicating whether ComicVine features are available</returns>
        [HttpGet("status")]
        [AllowAnonymous]
        public IActionResult GetStatus()
        {
            var isConfigured = _comicVineService.IsConfigured;
            
            return Ok(ApiResponse<object>.SuccessResponse(new 
            { 
                IsConfigured = isConfigured,
                Message = isConfigured 
                    ? "ComicVine API is configured and ready to use"
                    : "ComicVine API is not configured. Get a free API key at https://comicvine.gamespot.com/api/ and add it to appsettings.Development.json"
            }, isConfigured ? "ComicVine is available" : "ComicVine is not configured"));
        }

        /// <summary>
        /// Search ComicVine database for comics, characters, volumes, etc.
        /// </summary>
        /// <param name="query">Search query string</param>
        /// <param name="resourceType">Optional: filter by resource type (Issue, Volume, Character, etc.)</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Results per page (default: 10, max: 100)</param>
        /// <returns>Paginated search results</returns>
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(
            [FromQuery] string query,
            [FromQuery] ComicVineResourceType? resourceType = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Search query is required"));
                }

                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 100) pageSize = 100;

                var request = new ComicVineSearchRequest
                {
                    Query = query,
                    ResourceType = resourceType,
                    Page = page,
                    PageSize = pageSize
                };

                var results = await _comicVineService.SearchAsync(request);
                
                return Ok(ApiResponse<ComicVineSearchResponse>.SuccessResponse(results, "Search completed successfully"));
            }
            catch (InvalidOperationException ex)
            {
                // API not configured
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching ComicVine for: {Query}", query);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while searching ComicVine"));
            }
        }

        /// <summary>
        /// Search specifically for comic issues in ComicVine
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Results per page</param>
        [HttpGet("search/issues")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchIssues(
            [FromQuery] string query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Search query is required"));
                }

                var results = await _comicVineService.SearchIssuesAsync(query, page, pageSize);
                
                return Ok(ApiResponse<ComicVineSearchResponse>.SuccessResponse(results, "Issue search completed successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching ComicVine issues for: {Query}", query);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while searching ComicVine issues"));
            }
        }

        /// <summary>
        /// Search specifically for volumes (comic series) in ComicVine
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Results per page</param>
        [HttpGet("search/volumes")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchVolumes(
            [FromQuery] string query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Search query is required"));
                }

                var results = await _comicVineService.SearchVolumesAsync(query, page, pageSize);
                
                return Ok(ApiResponse<ComicVineSearchResponse>.SuccessResponse(results, "Volume search completed successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching ComicVine volumes for: {Query}", query);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while searching ComicVine volumes"));
            }
        }

        /// <summary>
        /// Get detailed information about a specific issue from ComicVine
        /// </summary>
        /// <param name="id">ComicVine issue ID</param>
        [HttpGet("issue/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetIssue(int id)
        {
            try
            {
                var issue = await _comicVineService.GetIssueAsync(id);
                
                if (issue == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Issue with ID {id} not found"));
                }

                return Ok(ApiResponse<ComicVineIssueDto>.SuccessResponse(issue, "Issue retrieved successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ComicVine issue: {IssueId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while retrieving the issue"));
            }
        }

        /// <summary>
        /// Get detailed information about a volume (comic series) from ComicVine
        /// </summary>
        /// <param name="id">ComicVine volume ID</param>
        [HttpGet("volume/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVolume(int id)
        {
            try
            {
                var volume = await _comicVineService.GetVolumeAsync(id);
                
                if (volume == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Volume with ID {id} not found"));
                }

                return Ok(ApiResponse<ComicVineVolumeDto>.SuccessResponse(volume, "Volume retrieved successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ComicVine volume: {VolumeId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while retrieving the volume"));
            }
        }

        /// <summary>
        /// Get all issues for a specific volume
        /// </summary>
        /// <param name="id">ComicVine volume ID</param>
        [HttpGet("volume/{id}/issues")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVolumeIssues(int id)
        {
            try
            {
                var issues = await _comicVineService.GetVolumeIssuesAsync(id);
                
                return Ok(ApiResponse<List<ComicVineIssueReferenceDto>>.SuccessResponse(issues, "Volume issues retrieved successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting issues for ComicVine volume: {VolumeId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while retrieving volume issues"));
            }
        }

        /// <summary>
        /// Get detailed information about a character from ComicVine
        /// </summary>
        /// <param name="id">ComicVine character ID</param>
        [HttpGet("character/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCharacter(int id)
        {
            try
            {
                var character = await _comicVineService.GetCharacterAsync(id);
                
                if (character == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse($"Character with ID {id} not found"));
                }

                return Ok(ApiResponse<ComicVineCharacterDto>.SuccessResponse(character, "Character retrieved successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ComicVine character: {CharacterId}", id);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while retrieving the character"));
            }
        }

        /// <summary>
        /// Import a comic issue from ComicVine into user's personal collection
        /// </summary>
        /// <param name="request">Import request with issue ID and collection details</param>
        [HttpPost("import")]
        public async Task<IActionResult> ImportToCollection([FromBody] ImportFromComicVineRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                var comic = await _comicVineService.ImportIssueToCollectionAsync(request.ComicVineIssueId, userId, request);
                
                return CreatedAtAction(
                    nameof(GetIssue), 
                    new { id = request.ComicVineIssueId }, 
                    ApiResponse<ComicDto>.SuccessResponse(comic, "Comic imported to collection successfully"));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing ComicVine issue {IssueId} for user {UserId}", 
                    request.ComicVineIssueId, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An error occurred while importing the comic"));
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user token");
            }
            return userId;
        }
    }
}
