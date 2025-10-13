using comicTracker.DTOs;
using comicTracker.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;

namespace comicTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var token = await GenerateJwtToken(user);
                var userDto = _mapper.Map<UserDto>(user);

                return new AuthResult
                {
                    Success = true,
                    Message = "User registered successfully",
                    Token = token,
                    User = userDto
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

                var token = await GenerateJwtToken(user);
                var userDto = _mapper.Map<UserDto>(user);

                return new AuthResult
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    User = userDto
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
            // For now, returning not implemented. 
            // In a production app, you'd implement refresh token logic
            await Task.CompletedTask;
            return new AuthResult
            {
                Success = false,
                Message = "Refresh token not implemented",
                Errors = new List<string> { "Feature not implemented" }
            };
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

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };

            // Add user roles if any
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}