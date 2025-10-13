using Microsoft.AspNetCore.Identity;

namespace comicTracker.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? AvatarUrl { get; set; }
        
        // Navigation properties
        public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
    }
}