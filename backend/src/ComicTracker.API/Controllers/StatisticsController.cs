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
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }

        /// <summary>
        /// Get comprehensive user statistics including total comics, series, publishers, and total value
        /// </summary>
        /// <returns>User collection statistics</returns>
        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview()
        {
            try
            {
                var userId = GetCurrentUserId();
                var statistics = await _statisticsService.GetUserStatisticsAsync(userId);

                return Ok(ApiResponse<UserStatistics>.SuccessResponse(statistics, "Statistics retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting statistics overview for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get breakdown of comics by publisher
        /// </summary>
        /// <returns>Publisher breakdown statistics</returns>
        [HttpGet("by-publisher")]
        public async Task<IActionResult> GetPublisherBreakdown()
        {
            try
            {
                var userId = GetCurrentUserId();
                var breakdown = await _statisticsService.GetPublisherBreakdownAsync(userId);

                return Ok(ApiResponse<PublisherBreakdown>.SuccessResponse(breakdown, "Publisher breakdown retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting publisher breakdown for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get breakdown of comics by condition
        /// </summary>
        /// <returns>Condition breakdown statistics</returns>
        [HttpGet("by-condition")]
        public async Task<IActionResult> GetConditionBreakdown()
        {
            try
            {
                var userId = GetCurrentUserId();
                var breakdown = await _statisticsService.GetConditionBreakdownAsync(userId);

                return Ok(ApiResponse<ConditionBreakdown>.SuccessResponse(breakdown, "Condition breakdown retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting condition breakdown for user {UserId}", GetCurrentUserId());
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