using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using comicTracker.DTOs;
using comicTracker.Models;
using AutoMapper;

namespace comicTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all users (Admin only)
        /// </summary>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                if (!await IsCurrentUserAdmin())
                {
                    return Forbid("Admin access required");
                }

                var users = await _userManager.Users
                    .Include(u => u.Comics)
                    .OrderBy(u => u.UserName)
                    .ToListAsync();

                var userDtos = _mapper.Map<List<UserDto>>(users);
                return Ok(ApiResponse<List<UserDto>>.SuccessResponse(userDtos, "Users retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get user with all their comics (Admin only)
        /// </summary>
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserWithComics(int userId)
        {
            try
            {
                if (!await IsCurrentUserAdmin())
                {
                    return Forbid("Admin access required");
                }

                var user = await _userManager.Users
                    .Include(u => u.Comics)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                var userDto = _mapper.Map<UserWithComicsDto>(user);
                return Ok(ApiResponse<UserWithComicsDto>.SuccessResponse(userDto, "User retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user {UserId}", userId);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Update any user (Admin only)
        /// </summary>
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] AdminUserUpdateRequest request)
        {
            try
            {
                if (!await IsCurrentUserAdmin())
                {
                    return Forbid("Admin access required");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                // Check if username/email are being changed and if they're already taken
                if (!string.Equals(user.UserName, request.Username, StringComparison.OrdinalIgnoreCase))
                {
                    var existingUser = await _userManager.FindByNameAsync(request.Username);
                    if (existingUser != null && existingUser.Id != userId)
                    {
                        return BadRequest(ApiResponse<object>.ErrorResponse("Username is already taken"));
                    }
                }

                if (!string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var existingUser = await _userManager.FindByEmailAsync(request.Email);
                    if (existingUser != null && existingUser.Id != userId)
                    {
                        return BadRequest(ApiResponse<object>.ErrorResponse("Email is already taken"));
                    }
                }

                _mapper.Map(request, user);
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Failed to update user",
                        result.Errors.Select(e => e.Description).ToList()));
                }

                var updatedUser = await _userManager.Users
                    .Include(u => u.Comics)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var userDto = _mapper.Map<UserDto>(updatedUser);
                return Ok(ApiResponse<UserDto>.SuccessResponse(userDto, "User updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}", userId);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Delete a user (Admin only)
        /// </summary>
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                if (!await IsCurrentUserAdmin())
                {
                    return Forbid("Admin access required");
                }

                var currentUserId = GetCurrentUserId();
                if (currentUserId == userId)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Cannot delete your own account"));
                }

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Failed to delete user",
                        result.Errors.Select(e => e.Description).ToList()));
                }

                return Ok(ApiResponse<string>.SuccessResponse("User deleted successfully", "User deleted successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}", userId);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Get system statistics (Admin only)
        /// </summary>
        [HttpGet("statistics")]
        public async Task<IActionResult> GetSystemStatistics()
        {
            try
            {
                if (!await IsCurrentUserAdmin())
                {
                    return Forbid("Admin access required");
                }

                var totalUsers = await _userManager.Users.CountAsync();
                var totalAdmins = await _userManager.Users.CountAsync(u => u.IsAdmin);
                var totalComics = await _userManager.Users
                    .SelectMany(u => u.Comics)
                    .CountAsync();

                var recentUsers = await _userManager.Users
                    .OrderByDescending(u => u.DateCreated)
                    .Take(5)
                    .Select(u => new { u.UserName, u.DateCreated })
                    .ToListAsync();

                var statistics = new
                {
                    TotalUsers = totalUsers,
                    TotalAdmins = totalAdmins,
                    TotalComics = totalComics,
                    RecentUsers = recentUsers
                };

                return Ok(ApiResponse<object>.SuccessResponse(statistics, "System statistics retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving system statistics");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        private async Task<bool> IsCurrentUserAdmin()
        {
            var userId = GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user?.IsAdmin ?? false;
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