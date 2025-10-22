using Microsoft.AspNetCore.Identity;

namespace comicTracker.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? AvatarUrl { get; set; }
        
        // Role-based authentication
        public UserRole Role { get; set; } = UserRole.User;
        
        // Legacy admin flag for backward compatibility (will be deprecated)
        public bool IsAdmin => Role >= UserRole.Admin;
        
        // Navigation properties
        public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}