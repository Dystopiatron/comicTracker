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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly ILogger<WishlistController> _logger;

        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService;
            _logger = logger;
        }

    
        /// <returns>Paginated list of wishlist items</returns>
        [HttpGet]
        public async Task<IActionResult> GetWishlist([FromQuery] WishlistQuery query)
        {
            try
            {
                var userId = GetCurrentUserId();
                var wishlistItems = await _wishlistService.GetUserWishlistAsync(userId, query);
                
                return Ok(ApiResponse<PagedResult<WishlistItemDto>>.SuccessResponse(wishlistItems, "Wishlist retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wishlist for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get a specific wishlist item by ID
        /// </summary>
        /// <param name="id">Wishlist item ID</param>
        /// <returns>Wishlist item details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishlistItem(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var wishlistItem = await _wishlistService.GetWishlistItemByIdAsync(id, userId);
                
                if (wishlistItem == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Wishlist item not found"));
                }

                return Ok(ApiResponse<WishlistItemDto>.SuccessResponse(wishlistItem, "Wishlist item retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wishlist item {ItemId} for user {UserId}", id, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Create a new wishlist item
        /// </summary>
        /// <param name="request">Wishlist item creation details</param>
        /// <returns>Created wishlist item</returns>
        [HttpPost]
        public async Task<IActionResult> CreateWishlistItem([FromBody] CreateWishlistItemRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var userId = GetCurrentUserId();
                var wishlistItem = await _wishlistService.CreateWishlistItemAsync(request, userId);

                return CreatedAtAction(nameof(GetWishlistItem), new { id = wishlistItem.Id }, 
                    ApiResponse<WishlistItemDto>.SuccessResponse(wishlistItem, "Wishlist item created successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating wishlist item for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Update an existing wishlist item
        /// </summary>
        /// <param name="id">Wishlist item ID</param>
        /// <param name="request">Updated wishlist item details</param>
        /// <returns>Updated wishlist item</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWishlistItem(int id, [FromBody] UpdateWishlistItemRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var userId = GetCurrentUserId();
                var wishlistItem = await _wishlistService.UpdateWishlistItemAsync(id, request, userId);

                if (wishlistItem == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Wishlist item not found"));
                }

                return Ok(ApiResponse<WishlistItemDto>.SuccessResponse(wishlistItem, "Wishlist item updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating wishlist item {ItemId} for user {UserId}", id, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Delete a wishlist item
        /// </summary>
        /// <param name="id">Wishlist item ID</param>
        /// <returns>Success message</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlistItem(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _wishlistService.DeleteWishlistItemAsync(id, userId);

                if (!success)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Wishlist item not found"));
                }

                return Ok(ApiResponse<string>.SuccessResponse("Wishlist item deleted successfully", "Wishlist item deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting wishlist item {ItemId} for user {UserId}", id, GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Convert a wishlist item to an owned comic
        /// </summary>
        /// <param name="id">Wishlist item ID</param>
        /// <param name="request">Conversion details</param>
        /// <returns>Created comic</returns>
        [HttpPost("{id}/convert")]
        public async Task<IActionResult> ConvertToOwned(int id, [FromBody] ConvertToOwnedRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var userId = GetCurrentUserId();
                var comic = await _wishlistService.ConvertToOwnedAsync(id, request, userId);

                if (comic == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Wishlist item not found"));
                }

                return Ok(ApiResponse<ComicDto>.SuccessResponse(comic, "Wishlist item converted to owned comic successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting wishlist item {ItemId} to owned for user {UserId}", id, GetCurrentUserId());
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
