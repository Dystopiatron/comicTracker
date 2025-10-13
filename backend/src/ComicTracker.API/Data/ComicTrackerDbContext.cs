using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using comicTracker.Models;

namespace comicTracker.Data
{
    public class ComicTrackerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ComicTrackerDbContext(DbContextOptions<ComicTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<Comic> Comics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Comic entity
            builder.Entity<Comic>(entity =>
            {
                entity.HasKey(c => c.Id);
                
                entity.Property(c => c.SeriesName)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(c => c.IssueNumber)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(c => c.Publisher)
                    .HasMaxLength(100);
                
                entity.Property(c => c.Condition)
                    .IsRequired()
                    .HasConversion<string>();
                
                entity.Property(c => c.PurchasePrice)
                    .HasColumnType("decimal(18,2)");
                
                entity.Property(c => c.CoverImageUrl)
                    .HasMaxLength(500);
                
                entity.Property(c => c.Notes)
                    .HasMaxLength(1000);
                
                // Configure relationship
                entity.HasOne(c => c.User)
                    .WithMany(u => u.Comics)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Index for better performance
                entity.HasIndex(c => c.UserId);
                entity.HasIndex(c => c.SeriesName);
                entity.HasIndex(c => c.Publisher);
            });

            // Configure ApplicationUser
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(u => u.AvatarUrl)
                    .HasMaxLength(500);
            });

            // Seed some initial data for demo purposes
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Create a demo user
            var hasher = new PasswordHasher<ApplicationUser>();
            var demoUser = new ApplicationUser
            {
                Id = 1,
                UserName = "demouser",
                NormalizedUserName = "DEMOUSER",
                Email = "demo@comictracker.com",
                NormalizedEmail = "DEMO@COMICTRACKER.COM",
                EmailConfirmed = true,
                FirstName = "Demo",
                LastName = "User",
                DateCreated = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            demoUser.PasswordHash = hasher.HashPassword(demoUser, "Demo123!");

            // Create comicfan user
            var comicFanUser = new ApplicationUser
            {
                Id = 2,
                UserName = "comicfan",
                NormalizedUserName = "COMICFAN",
                Email = "fan@comictracker.com",
                NormalizedEmail = "FAN@COMICTRACKER.COM",
                EmailConfirmed = true,
                FirstName = "Comic",
                LastName = "Fan",
                DateCreated = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            comicFanUser.PasswordHash = hasher.HashPassword(comicFanUser, "MyComics2024");

            builder.Entity<ApplicationUser>().HasData(demoUser, comicFanUser);

            // Seed some comics
            builder.Entity<Comic>().HasData(
                new Comic
                {
                    Id = 1,
                    SeriesName = "Amazing Spider-Man",
                    IssueNumber = "#300",
                    Publisher = "Marvel",
                    Condition = ComicCondition.NearMint,
                    PurchasePrice = 25.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-30),
                    UserId = 1,
                    Notes = "First appearance of Venom"
                },
                new Comic
                {
                    Id = 2,
                    SeriesName = "Batman",
                    IssueNumber = "#1",
                    Publisher = "DC",
                    Condition = ComicCondition.VeryFine,
                    PurchasePrice = 50.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-25),
                    UserId = 1,
                    Notes = "Classic Batman issue"
                },
                new Comic
                {
                    Id = 3,
                    SeriesName = "The Walking Dead",
                    IssueNumber = "#1",
                    Publisher = "Image",
                    Condition = ComicCondition.Mint,
                    PurchasePrice = 100.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-20),
                    UserId = 1,
                    Notes = "First issue of the series"
                },
                new Comic
                {
                    Id = 4,
                    SeriesName = "X-Men",
                    IssueNumber = "#1",
                    Publisher = "Marvel",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 75.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-15),
                    UserId = 1,
                    Notes = "First X-Men comic"
                },
                new Comic
                {
                    Id = 5,
                    SeriesName = "Superman",
                    IssueNumber = "#1",
                    Publisher = "DC",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 60.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-10),
                    UserId = 1,
                    Notes = "Man of Steel"
                },
                
                // Comics for comicfan user (UserId = 2)
                new Comic
                {
                    Id = 6,
                    SeriesName = "Spider-Man",
                    IssueNumber = "#300",
                    Publisher = "Marvel",
                    Condition = ComicCondition.VeryFine,
                    PurchasePrice = 50.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-100),
                    UserId = 2,
                    Notes = "Classic black suit issue"
                },
                new Comic
                {
                    Id = 7,
                    SeriesName = "Batman: The Killing Joke",
                    IssueNumber = "#1",
                    Publisher = "DC",
                    Condition = ComicCondition.NearMint,
                    PurchasePrice = 75.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-95),
                    UserId = 2,
                    Notes = "Iconic Joker story"
                },
                new Comic
                {
                    Id = 8,
                    SeriesName = "Saga",
                    IssueNumber = "#1",
                    Publisher = "Image",
                    Condition = ComicCondition.Mint,
                    PurchasePrice = 3.99m,
                    DateAdded = DateTime.UtcNow.AddDays(-90),
                    UserId = 2,
                    Notes = "First issue of acclaimed series"
                },
                new Comic
                {
                    Id = 9,
                    SeriesName = "Watchmen",
                    IssueNumber = "#1",
                    Publisher = "DC",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 1.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-85),
                    UserId = 2,
                    Notes = "Seminal graphic novel"
                },
                new Comic
                {
                    Id = 10,
                    SeriesName = "The Walking Dead",
                    IssueNumber = "#1",
                    Publisher = "Image",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-80),
                    UserId = 2,
                    Notes = "First zombie apocalypse issue"
                },
                new Comic
                {
                    Id = 11,
                    SeriesName = "X-Men",
                    IssueNumber = "#141",
                    Publisher = "Marvel",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 0.60m,
                    DateAdded = DateTime.UtcNow.AddDays(-75),
                    UserId = 2,
                    Notes = "Days of Future Past begins"
                },
                new Comic
                {
                    Id = 12,
                    SeriesName = "Hellboy: Seed of Destruction",
                    IssueNumber = "#1",
                    Publisher = "Dark Horse",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-70),
                    UserId = 2,
                    Notes = "Hellboy's first appearance"
                },
                new Comic
                {
                    Id = 13,
                    SeriesName = "Teenage Mutant Ninja Turtles",
                    IssueNumber = "#1",
                    Publisher = "Mirage Studios",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 1.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-65),
                    UserId = 2,
                    Notes = "First TMNT appearance"
                },
                new Comic
                {
                    Id = 14,
                    SeriesName = "Sandman",
                    IssueNumber = "#1",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.VeryFine,
                    PurchasePrice = 2.00m,
                    DateAdded = DateTime.UtcNow.AddDays(-60),
                    UserId = 2,
                    Notes = "Neil Gaiman masterpiece"
                },
                new Comic
                {
                    Id = 15,
                    SeriesName = "Y: The Last Man",
                    IssueNumber = "#1",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.NearMint,
                    PurchasePrice = 2.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-55),
                    UserId = 2,
                    Notes = "Post-apocalyptic drama"
                },
                new Comic
                {
                    Id = 16,
                    SeriesName = "Bone",
                    IssueNumber = "#1",
                    Publisher = "Cartoon Books",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 1.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-50),
                    UserId = 2,
                    Notes = "Jeff Smith's epic fantasy"
                },
                new Comic
                {
                    Id = 17,
                    SeriesName = "Maus",
                    IssueNumber = "#1",
                    Publisher = "Pantheon Books",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 5.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-45),
                    UserId = 2,
                    Notes = "Pulitzer Prize winner"
                },
                new Comic
                {
                    Id = 18,
                    SeriesName = "Preacher",
                    IssueNumber = "#1",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-40),
                    UserId = 2,
                    Notes = "Garth Ennis classic"
                },
                new Comic
                {
                    Id = 19,
                    SeriesName = "Invincible",
                    IssueNumber = "#1",
                    Publisher = "Image",
                    Condition = ComicCondition.NearMint,
                    PurchasePrice = 2.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-35),
                    UserId = 2,
                    Notes = "Robert Kirkman superhero saga"
                },
                new Comic
                {
                    Id = 20,
                    SeriesName = "Fantastic Four",
                    IssueNumber = "#48",
                    Publisher = "Marvel",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 0.12m,
                    DateAdded = DateTime.UtcNow.AddDays(-30),
                    UserId = 2,
                    Notes = "First Silver Surfer"
                },
                new Comic
                {
                    Id = 21,
                    SeriesName = "V for Vendetta",
                    IssueNumber = "#1",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Good,
                    PurchasePrice = 1.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-25),
                    UserId = 2,
                    Notes = "Alan Moore dystopian tale"
                },
                new Comic
                {
                    Id = 22,
                    SeriesName = "The Incredible Hulk",
                    IssueNumber = "#181",
                    Publisher = "Marvel",
                    Condition = ComicCondition.Fair,
                    PurchasePrice = 0.25m,
                    DateAdded = DateTime.UtcNow.AddDays(-20),
                    UserId = 2,
                    Notes = "First Wolverine appearance"
                },
                new Comic
                {
                    Id = 23,
                    SeriesName = "Akira",
                    IssueNumber = "#1",
                    Publisher = "Marvel Epic",
                    Condition = ComicCondition.VeryFine,
                    PurchasePrice = 1.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-15),
                    UserId = 2,
                    Notes = "Katsuhiro Otomo cyberpunk"
                },
                new Comic
                {
                    Id = 24,
                    SeriesName = "Spawn",
                    IssueNumber = "#1",
                    Publisher = "Image",
                    Condition = ComicCondition.NearMint,
                    PurchasePrice = 1.95m,
                    DateAdded = DateTime.UtcNow.AddDays(-10),
                    UserId = 2,
                    Notes = "Todd McFarlane creation"
                },
                new Comic
                {
                    Id = 25,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#1",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-5),
                    UserId = 2,
                    Notes = "Warren Ellis cyberpunk journalism"
                },
                new Comic
                {
                    Id = 26,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#2",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-4),
                    UserId = 2,
                    Notes = "On the Stump. Spider Jerusalem covers a political rally."
                },
                new Comic
                {
                    Id = 27,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#3",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-3),
                    UserId = 2,
                    Notes = "Wild in the Country. Spider investigates cryogenic revivals."
                },
                new Comic
                {
                    Id = 28,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#4",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-2),
                    UserId = 2,
                    Notes = "New City. Spider explores the city's underbelly."
                },
                new Comic
                {
                    Id = 29,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#5",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddDays(-1),
                    UserId = 2,
                    Notes = "What Spider Watches on TV. Media criticism and satire."
                },
                new Comic
                {
                    Id = 30,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#6",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow,
                    UserId = 2,
                    Notes = "God Riding Shotgun. Spider investigates new religious movements."
                },
                new Comic
                {
                    Id = 31,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#7",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddHours(1),
                    UserId = 2,
                    Notes = "My Boyfriend is a Virus. Exploration of transhumanism."
                },
                new Comic
                {
                    Id = 32,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#8",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddHours(2),
                    UserId = 2,
                    Notes = "Another Cold Morning. More on cryogenic revivals and societal change."
                },
                new Comic
                {
                    Id = 33,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#9",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddHours(3),
                    UserId = 2,
                    Notes = "Party Time. Spider attends a political convention."
                },
                new Comic
                {
                    Id = 34,
                    SeriesName = "Transmetropolitan",
                    IssueNumber = "#10",
                    Publisher = "Vertigo",
                    Condition = ComicCondition.Fine,
                    PurchasePrice = 2.50m,
                    DateAdded = DateTime.UtcNow.AddHours(4),
                    UserId = 2,
                    Notes = "Freeze Me with Your Kiss. Exploration of future cryogenic technology."
                }
            );
        }
    }
}