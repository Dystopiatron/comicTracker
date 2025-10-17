using comicTracker.DTOs;
using comicTracker.Models;
using comicTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using AutoMapper;

namespace comicTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ComicTrackerDbContext _context;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ComicTrackerDbContext context,
            IRolePermissionService rolePermissionService,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _rolePermissionService = rolePermissionService;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "User with this email already exists",
                        Errors = new List<string> { "Email is already registered" }
                    };
                }

                var existingUsername = await _userManager.FindByNameAsync(request.Username);
                if (existingUsername != null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Username is already taken",
                        Errors = new List<string> { "Username is already taken" }
                    };
                }

                // Create new user
                var user = new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateCreated = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Failed to create user",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                // Generate JWT token
                var tokenExpiry = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60"));
                var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                
                var token = await GenerateJwtToken(user, tokenExpiry);
                var refreshToken = await GenerateRefreshToken(user, refreshTokenExpiry);
                var userDto = MapUserToDto(user);

                return new AuthResult
                {
                    Success = true,
                    Message = "User registered successfully",
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    User = userDto,
                    TokenExpiry = tokenExpiry,
                    RefreshTokenExpiry = refreshTokenExpiry
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration");
                return new AuthResult
                {
                    Success = false,
                    Message = "An error occurred during registration",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Invalid username or password",
                        Errors = new List<string> { "Invalid credentials" }
                    };
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Invalid username or password",
                        Errors = new List<string> { "Invalid credentials" }
                    };
                }

                // Generate tokens
                var tokenExpiry = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60"));
                var refreshTokenExpiry = request.RememberMe ? 
                    DateTime.UtcNow.AddDays(30) : 
                    DateTime.UtcNow.AddDays(7);

                var token = await GenerateJwtToken(user, tokenExpiry);
                var refreshToken = await GenerateRefreshToken(user, refreshTokenExpiry);
                var userDto = MapUserToDto(user);

                return new AuthResult
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    User = userDto,
                    TokenExpiry = tokenExpiry,
                    RefreshTokenExpiry = refreshTokenExpiry
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                return new AuthResult
                {
                    Success = false,
                    Message = "An error occurred during login",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var storedToken = await _context.RefreshTokens
                    .Include(rt => rt.User)
                    .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

                if (storedToken == null || !storedToken.IsActive)
                {
                    return new AuthResult
                    {
                        Success = false,
                        Message = "Invalid or expired refresh token",
                        Errors = new List<string> { "Token not found or expired" }
                    };
                }

                // Revoke the used refresh token
                storedToken.IsRevoked = true;
                storedToken.RevokedReason = "Used for token refresh";

                // Generate new tokens
                var tokenExpiry = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60"));
                var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);

                var newToken = await GenerateJwtToken(storedToken.User, tokenExpiry);
                var newRefreshToken = await GenerateRefreshToken(storedToken.User, refreshTokenExpiry);
                var userDto = MapUserToDto(storedToken.User);

                await _context.SaveChangesAsync();

                return new AuthResult
                {
                    Success = true,
                    Message = "Token refreshed successfully",
                    Token = newToken,
                    RefreshToken = newRefreshToken.Token,
                    User = userDto,
                    TokenExpiry = tokenExpiry,
                    RefreshTokenExpiry = refreshTokenExpiry
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during token refresh");
                return new AuthResult
                {
                    Success = false,
                    Message = "An error occurred during token refresh",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<bool> LogoutAsync(string userId)
        {
            try
            {
                // In a stateless JWT implementation, logout is typically handled client-side
                // You might implement token blacklisting here if needed
                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during logout for user {UserId}", userId);
                return false;
            }
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user, DateTime? expiry = null)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var tokenExpiry = expiry ?? DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "60"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("isAdmin", user.IsAdmin.ToString().ToLower()),
                new Claim("role", user.Role.ToString()),
                new Claim("roleDisplayName", _rolePermissionService.GetRoleDisplayName(user.Role))
            };

            // Add permission claims
            var permissions = _rolePermissionService.GetPermissions(user.Role);
            claims.AddRange(permissions.Select(permission => new Claim("permission", permission.ToString())));

            // Add user roles if any
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpiry,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user, DateTime expiry)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateSecureRandomToken(),
                ExpiryDate = expiry,
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        private static string GenerateSecureRandomToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[64];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
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

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken, string? reason = null)
        {
            try
            {
                var storedToken = await _context.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

                if (storedToken == null || storedToken.IsRevoked)
                {
                    return false;
                }

                storedToken.IsRevoked = true;
                storedToken.RevokedReason = reason ?? "Manually revoked";
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking refresh token");
                return false;
            }
        }
    }
}