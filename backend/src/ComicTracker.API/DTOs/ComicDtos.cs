using comicTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace comicTracker.DTOs
{
    public class ComicDto
    {
        public int Id { get; set; }
        public string SeriesName { get; set; } = string.Empty;
        public string IssueNumber { get; set; } = string.Empty;
        public string? Publisher { get; set; }
        public string Condition { get; set; } = string.Empty;
        public decimal? PurchasePrice { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public class CreateComicRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string SeriesName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string IssueNumber { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Publisher { get; set; }
        
        [Required]
        public ComicCondition Condition { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Purchase price must be a positive value")]
        public decimal? PurchasePrice { get; set; }
        
        [StringLength(500)]
        [Url(ErrorMessage = "Please provide a valid URL")]
        public string? CoverImageUrl { get; set; }
        
        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    public class UpdateComicRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string SeriesName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string IssueNumber { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Publisher { get; set; }
        
        [Required]
        public ComicCondition Condition { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Purchase price must be a positive value")]
        public decimal? PurchasePrice { get; set; }
        
        [StringLength(500)]
        [Url(ErrorMessage = "Please provide a valid URL")]
        public string? CoverImageUrl { get; set; }
        
        [StringLength(1000)]
        public string? Notes { get; set; }
    }

    public class ComicQuery
    {
        public string? Search { get; set; }
        public string? Publisher { get; set; }
        public ComicCondition? Condition { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "seriesname";
        public string SortDirection { get; set; } = "asc";
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }
}