using comicTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace comicTracker.DTOs
{
    public class WishlistItemDto
    {
        public int Id { get; set; }
        public string SeriesName { get; set; } = string.Empty;
        public string IssueNumber { get; set; } = string.Empty;
        public string? Publisher { get; set; }
        public string? DesiredCondition { get; set; }
        public decimal? TargetPrice { get; set; }
        public int Priority { get; set; }
        public string PriorityLabel { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public class CreateWishlistItemRequest
    {
        [Required(ErrorMessage = "Series name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Series name must be between 1 and 200 characters")]
        public string SeriesName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Issue number is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Issue number must be between 1 and 50 characters")]
        public string IssueNumber { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Publisher name cannot exceed 100 characters")]
        public string? Publisher { get; set; }
        
        public ComicCondition? DesiredCondition { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Target price must be a positive value")]
        public decimal? TargetPrice { get; set; }
        
        [Range(1, 4, ErrorMessage = "Priority must be between 1 (Must Have) and 4 (Low)")]
        public int Priority { get; set; } = 3;
        
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }
    }

    public class UpdateWishlistItemRequest
    {
        [Required(ErrorMessage = "Series name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Series name must be between 1 and 200 characters")]
        public string SeriesName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Issue number is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Issue number must be between 1 and 50 characters")]
        public string IssueNumber { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Publisher name cannot exceed 100 characters")]
        public string? Publisher { get; set; }
        
        public ComicCondition? DesiredCondition { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Target price must be a positive value")]
        public decimal? TargetPrice { get; set; }
        
        [Range(1, 4, ErrorMessage = "Priority must be between 1 (Must Have) and 4 (Low)")]
        public int Priority { get; set; }
        
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }
    }

    public class WishlistQuery
    {
        public string? Search { get; set; }
        public string? Publisher { get; set; }
        public int? Priority { get; set; }
        public ComicCondition? DesiredCondition { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "priority";
        public string SortDirection { get; set; } = "asc";
    }

    public class ConvertToOwnedRequest
    {
        [Required]
        public ComicCondition ActualCondition { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Purchase price must be a positive value")]
        public decimal? ActualPrice { get; set; }
        
        [StringLength(500)]
        [Url(ErrorMessage = "Please provide a valid URL")]
        public string? CoverImageUrl { get; set; }
        
        [StringLength(1000)]
        public string? Notes { get; set; }
    }
}
