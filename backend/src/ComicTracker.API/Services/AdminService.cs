using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using comicTracker.Models;

namespace comicTracker.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminService> _logger;

        public AdminService(UserManager<ApplicationUser> userManager, ILogger<AdminService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> EnsureAdminUserExistsAsync()
        {
            try
            {
                // Check if any admin users exist
                var adminExists = await _userManager.Users.AnyAsync(u => u.Role >= UserRole.Admin);
                
                if (!adminExists)
                {
                    // Create default admin user
                    var result = await CreateAdminUserAsync("comicfan", "fan@comictracker.com", "Admin123!");
                    if (result)
                    {
                        _logger.LogInformation("Default admin user created successfully");
                        return true;
                    }
                    else
                    {
                        _logger.LogError("Failed to create default admin user");
                        return false;
                    }
                }

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