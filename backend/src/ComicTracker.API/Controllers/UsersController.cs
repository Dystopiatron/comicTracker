using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using comicTracker.DTOs;
using comicTracker.Services;
using comicTracker.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace comicTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get current user profile
        /// </summary>
        /// <returns>User profile information</returns>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(ApiResponse<UserDto>.SuccessResponse(userDto, "Profile retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting profile for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="request">Updated profile information</param>
        /// <returns>Updated user profile</returns>
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var userId = GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                _mapper.Map(request, user);
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Failed to update profile",
                        result.Errors.Select(e => e.Description).ToList()));
                }

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(ApiResponse<UserDto>.SuccessResponse(userDto, "Profile updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", GetCurrentUserId());
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get current user information (alternative endpoint)
        /// </summary>
        /// <returns>Current user information</returns>
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userId = GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(ApiResponse<UserDto>.SuccessResponse(userDto, "User information retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user {UserId}", GetCurrentUserId());
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