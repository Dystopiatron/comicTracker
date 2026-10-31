using Microsoft.AspNetCore.Identity;
using comicTracker.Models;

namespace comicTracker.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminService> _logger;

        public AdminService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<AdminService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> EnsureAdminUserExistsAsync()
        {
            try
            {
                var username = _configuration["Admin:Username"]?.Trim();
                var password = _configuration["Admin:Password"];
                var email = _configuration["Admin:Email"]?.Trim();

                if (string.IsNullOrWhiteSpace(password))
                {
                    _logger.LogWarning(
                        "Skipping admin user seed because Admin:Password is not configured. Set Admin:Username and Admin:Password with user-secrets or the environment variables Admin__Username and Admin__Password. No default admin password is used.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(username))
                {
                    _logger.LogWarning(
                        "Skipping admin user seed because Admin:Username is not configured. Set Admin:Username and Admin:Password with user-secrets or the environment variables Admin__Username and Admin__Password.");
                    return false;
                }

                var existing = await _userManager.FindByNameAsync(username);
                if (existing != null)
                {
                    if (existing.Role < UserRole.Admin)
                    {
                        _logger.LogWarning(
                            "User {Username} already exists and is not an admin. Admin seed skipped and the account was not modified.",
                            username);
                    }
                    else
                    {
                        _logger.LogInformation("Admin user {Username} already exists. Admin seed skipped.", username);
                    }

                    return true;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    email = $"{username}@localhost";
                }

                var created = await CreateAdminUserAsync(username, email, password);
                if (!created)
                {
                    _logger.LogError("Failed to create admin user {Username} from configuration", username);
                    return false;
                }

                _logger.LogInformation("Admin user {Username} created from configuration", username);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ensuring admin user exists");
                return false;
            }
        }

        public async Task<bool> CreateAdminUserAsync(string username, string email, string password)
        {
            try
            {
                var adminUser = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                    Role = UserRole.Admin,
                    DateCreated = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(adminUser, password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("Admin user {Username} created successfully", username);
                    return true;
                }
                else
                {
                    _logger.LogError("Failed to create admin user {Username}. Errors: {Errors}", 
                        username, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating admin user {Username}", username);
                return false;
            }
        }
    }
}