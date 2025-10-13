using Microsoft.AspNetCore.Mvc;
using comicTracker.DTOs;
using comicTracker.Services;

namespace comicTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <returns>Authentication result with JWT token</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data", 
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var result = await _authService.RegisterAsync(request);
                
                if (result.Success)
                {
                    return Ok(ApiResponse<AuthResult>.SuccessResponse(result, "User registered successfully"));
                }

                return BadRequest(ApiResponse<object>.ErrorResponse(result.Message, result.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>Authentication result with JWT token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data",
                        ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
                }

                var result = await _authService.LoginAsync(request);
                
                if (result.Success)
                {
                    return Ok(ApiResponse<AuthResult>.SuccessResponse(result, "Login successful"));
                }

                return Unauthorized(ApiResponse<object>.ErrorResponse(result.Message, result.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Refresh JWT token
        /// </summary>
        /// <param name="request">Refresh token request</param>
        /// <returns>New JWT token</returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid request data"));
                }

                var result = await _authService.RefreshTokenAsync(request.RefreshToken);
                
                if (result.Success)
                {
                    return Ok(ApiResponse<AuthResult>.SuccessResponse(result, "Token refreshed successfully"));
                }

                return BadRequest(ApiResponse<object>.ErrorResponse(result.Message, result.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during token refresh");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }

        /// <summary>
        /// Logout user (placeholder for client-side token removal)
        /// </summary>
        /// <returns>Success message</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // In JWT implementation, logout is typically handled client-side
                // This endpoint exists for consistency and potential future token blacklisting
                await Task.CompletedTask;
                
                return Ok(ApiResponse<string>.SuccessResponse("Logout successful", "Logout successful"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return StatusCode(500, ApiResponse<object>.ErrorResponse("An internal error occurred"));
            }
        }
    }
}