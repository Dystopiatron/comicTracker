using comicTracker.Data;
using comicTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace comicTracker.Services
{
    public class DemoUserService : IDemoUserService
    {
        // Public, non-admin demo account. These dummy defaults are intentionally not secret.
        public const string DefaultUsername = "demo";
        public const string DefaultEmail = "demo@example.com";
        public const string DefaultPassword = "DemoUser123!";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ComicTrackerDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DemoUserService> _logger;

        public DemoUserService(
            UserManager<ApplicationUser> userManager,
            ComicTrackerDbContext context,
            IConfiguration configuration,
            ILogger<DemoUserService> logger)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task EnsureDemoUserExistsAsync()
        {
            try
            {
                if (!_configuration.GetValue("DemoUser:Enabled", true))
                {
                    _logger.LogInformation("Demo user seed is disabled (DemoUser:Enabled is false).");
                    return;
                }

                var username = _configuration["DemoUser:Username"];
                if (string.IsNullOrWhiteSpace(username))
                {
                    username = DefaultUsername;
                }

                username = username.Trim();

                var password = _configuration["DemoUser:Password"];
                if (string.IsNullOrWhiteSpace(password))
                {
                    password = DefaultPassword;
                }

                var email = _configuration["DemoUser:Email"];
                if (string.IsNullOrWhiteSpace(email))
                {
                    email = DefaultEmail;
                }

                email = email.Trim();

                var existing = await _userManager.FindByNameAsync(username);
                if (existing != null)
                {
                    if (existing.Role != UserRole.User)
                    {
                        _logger.LogWarning(
                            "Demo user seed skipped. {Username} already exists with role {Role}. The account was not modified.",
                            username,
                            existing.Role);
                    }
                    else
                    {
                        _logger.LogInformation("Demo user {Username} already exists. Demo seed skipped.", username);
                    }

                    return;
                }

                var emailOwner = await _userManager.FindByEmailAsync(email);
                if (emailOwner != null)
                {
                    _logger.LogWarning(
                        "Demo user seed skipped because {Email} is already registered to {Username}.",
                        email,
                        emailOwner.UserName);
                    return;
                }

                var demoUser = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = "Demo",
                    LastName = "Visitor",
                    Role = UserRole.User,
                    DateCreated = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(demoUser, password);
                if (!result.Succeeded)
                {
                    _logger.LogError(
                        "Failed to create demo user {Username}. Errors: {Errors}",
                        username,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }

                if (demoUser.Role != UserRole.User)
                {
                    _logger.LogError(
                        "Demo user {Username} was not stored with the normal user role ({Role}). The account was removed.",
                        username,
                        demoUser.Role);
                    await _userManager.DeleteAsync(demoUser);
                    return;
                }

                try
                {
                    await SeedSampleComicsAsync(demoUser.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Demo user {Username} was created, but sample comics could not be seeded.", username);
                    return;
                }

                _logger.LogInformation("Created public non-admin demo user {Username}.", username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ensuring demo user exists");
            }
        }

        private async Task SeedSampleComicsAsync(int userId)
        {
            var addedOn = DateTime.UtcNow;
            var samples = new[]
            {
                new Comic
                {
                    SeriesName = "Amazing Spider-Man",
                    IssueNumber = "#1",
                    Publisher = "Marvel",
                    Condition = ComicCondition.NearMint,
                    PurchasePrice = 4.99m,
                    Notes = "Sample issue for the public demo account",
                    DateAdded = addedOn,
                    UserId = userId
                },
                new Comic
                {
                    SeriesName = "Batman",
                    IssueNumber = "#1",
                    Publisher = "DC",
                    Condition = ComicCondition.VeryFine,
                    PurchasePrice = 5.99m,
                    Notes = "Sample issue for the public demo account",
                    DateAdded = addedOn,
                    UserId = userId
                },
                new Comic
                {
                    SeriesName = "Saga",
                    IssueNumber = "#1",
                    Publisher = "Image",
                    Condition = ComicCondition.Mint,
                    PurchasePrice = 3.99m,
                    Notes = "Sample issue for the public demo account",
                    DateAdded = addedOn,
                    UserId = userId
                }
            };

            _context.Comics.AddRange(samples);
            await _context.SaveChangesAsync();
        }
    }
}
