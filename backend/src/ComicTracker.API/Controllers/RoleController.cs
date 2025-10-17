using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using comicTracker.DTOs;
using comicTracker.Models;
using comicTracker.Data;
using comicTracker.Services;
using AutoMapper;

namespace comicTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ComicTrackerDbContext _context;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(
            UserManager<ApplicationUser> userManager,
            ComicTrackerDbContext context,
            IRolePermissionService rolePermissionService,
            IMapper mapper,
            ILogger<RoleController> logger)
        {
            _userManager = userManager;
            _context = context;
            _rolePermissionService = rolePermissionService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all available roles and their permissions
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                if (!await HasPermissionAsync(Permission.AccessAdminPanel))
                {
                    return Forbid("Admin access required");
                }

                var roles = Enum.GetValues<UserRole>()
                    .Select(role => new RoleInfoDto
                    {
                        Role = role,
                        DisplayName = _rolePermissionService.GetRoleDisplayName(role),
                        Permissions = _rolePermissionService.GetPermissions(role)
                            .Select(p => p.ToString())
                            .ToList()
                    })
                    .ToList();

                return Ok(ApiResponse<List<RoleInfoDto>>.SuccessResponse(roles, "Roles retrieved successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving roles");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Promote user to a new role (Admin+ only)
        /// </summary>
        [HttpPost("promote/{userId}")]
        public async Task<IActionResult> PromoteUser(int userId, [FromBody] PromoteUserRequest request)
        {
            try
            {
                if (!await HasPermissionAsync(Permission.PromoteUsers))
                {
                    return Forbid("Insufficient privileges to promote users");
                }

                var currentUser = await GetCurrentUserAsync();
                var targetUser = await _userManager.FindByIdAsync(userId.ToString());

                if (targetUser == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                // Prevent self-demotion
                if (currentUser.Id == targetUser.Id && request.NewRole < currentUser.Role)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Cannot demote yourself"));
                }

                // Prevent promoting above your own level
                if (request.NewRole >= currentUser.Role && currentUser.Role != UserRole.SuperAdmin)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Cannot promote user to same or higher role than yourself"));
                }

                var oldRole = targetUser.Role;
                targetUser.Role = request.NewRole;

                var result = await _userManager.UpdateAsync(targetUser);
                if (!result.Succeeded)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Failed to update user role",
                        result.Errors.Select(e => e.Description).ToList()));
                }

                _logger.LogInformation(
                    "User {CurrentUserId} promoted user {TargetUserId} from {OldRole} to {NewRole}. Reason: {Reason}",
                    currentUser.Id, targetUser.Id, oldRole, request.NewRole, request.Reason ?? "Not specified");

                var userDto = MapUserToDto(targetUser);
                return Ok(ApiResponse<UserDto>.SuccessResponse(userDto, "User role updated successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error promoting user {UserId}", userId);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Revoke all refresh tokens for a user (Admin+ only)
        /// </summary>
        [HttpPost("revoke-tokens/{userId}")]
        public async Task<IActionResult> RevokeUserTokens(int userId, [FromBody] RevokeTokenRequest request)
        {
            try
            {
                if (!await HasPermissionAsync(Permission.WriteUsers))
                {
                    return Forbid("Insufficient privileges to revoke user tokens");
                }

                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("User not found"));
                }

                var activeTokens = await _context.RefreshTokens
                    .Where(rt => rt.UserId == userId && rt.IsActive)
                    .ToListAsync();

                foreach (var token in activeTokens)
                {
                    token.IsRevoked = true;
                    token.RevokedReason = request.Reason ?? "Revoked by administrator";
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Admin {AdminId} revoked {TokenCount} tokens for user {UserId}. Reason: {Reason}",
                    GetCurrentUserId(), activeTokens.Count, userId, request.Reason ?? "Not specified");

                return Ok(ApiResponse<object>.SuccessResponse(
                    new { RevokedTokens = activeTokens.Count }, 
                    "User tokens revoked successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking tokens for user {UserId}", userId);
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        private async Task<bool> HasPermissionAsync(Permission permission)
        {
            var user = await GetCurrentUserAsync();
            return _rolePermissionService.HasPermission(user.Role, permission);
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UnauthorizedAccessException("User not found");
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

        private UserDto MapUserToDto(ApplicationUser user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.RoleDisplayName = _rolePermissionService.GetRoleDisplayName(user.Role);
            userDto.Permissions = _rolePermissionService.GetPermissions(user.Role)
                .Select(p => p.ToString())
                .ToList();
            
            return userDto;
        }
    }
}