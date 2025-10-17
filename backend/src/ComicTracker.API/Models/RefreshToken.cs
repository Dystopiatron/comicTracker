using System.ComponentModel.DataAnnotations;

namespace comicTracker.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        
        public string Token { get; set; } = string.Empty;
        
        public DateTime ExpiryDate { get; set; }
        
        public bool IsRevoked { get; set; } = false;
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public string? RevokedReason { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        
        // Helper properties
        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}