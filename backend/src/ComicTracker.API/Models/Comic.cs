using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comicTracker.Models
{
    public class Comic
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string SeriesName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string IssueNumber { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Publisher { get; set; }
        
        [Required]
        public ComicCondition Condition { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PurchasePrice { get; set; }
        
        [StringLength(500)]
        public string? CoverImageUrl { get; set; }
        
        [StringLength(1000)]
        public string? Notes { get; set; }
        
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        
        public DateTime? DateModified { get; set; }
        
        // Foreign key
        [Required]
        public int UserId { get; set; }
        
        // Navigation property
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}