using System.ComponentModel.DataAnnotations;

namespace comicTracker.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string SeriesName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string IssueNumber { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Publisher { get; set; }
        
        public ComicCondition? DesiredCondition { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal? TargetPrice { get; set; }
        
        [Range(1, 4)]
        public int Priority { get; set; } = 3; 
        
        [StringLength(1000)]
        public string? Notes { get; set; }
        
        public DateTime DateAdded { get; set; }
        
        public DateTime? DateModified { get; set; }
        
        // Navigation property
        public ApplicationUser User { get; set; } = null!;
    }
}
