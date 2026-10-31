namespace comicTracker.DTOs
{
    /// <summary>
    /// Search results from ComicVine API
    /// </summary>
    public class ComicVineSearchResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string ResourceType { get; set; } = string.Empty;
        public string? SiteDetailUrl { get; set; }
    }

    /// <summary>
    /// Issue details from ComicVine
    /// </summary>
    public class ComicVineIssueDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string IssueNumber { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? SmallImageUrl { get; set; }
        public DateTime? CoverDate { get; set; }
        public DateTime? StoreDate { get; set; }
        public ComicVineVolumeDto? Volume { get; set; }
        public List<ComicVinePersonCreditDto> PersonCredits { get; set; } = new();
        public List<ComicVineCharacterDto> Characters { get; set; } = new();
        public string? SiteDetailUrl { get; set; }
    }

    /// <summary>
    /// Volume (series) from ComicVine
    /// </summary>
    public class ComicVineVolumeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? StartYear { get; set; }
        public int? CountOfIssues { get; set; }
        public string? ImageUrl { get; set; }
        public ComicVinePublisherDto? Publisher { get; set; }
        public List<ComicVineIssueReferenceDto> Issues { get; set; } = new();
        public string? SiteDetailUrl { get; set; }
    }

    /// <summary>
    /// Publisher from ComicVine
    /// </summary>
    public class ComicVinePublisherDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? SiteDetailUrl { get; set; }
    }

    /// <summary>
    /// Character from ComicVine
    /// </summary>
    public class ComicVineCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? RealName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? CountOfIssueAppearances { get; set; }
        public string? SiteDetailUrl { get; set; }
    }

    /// <summary>
    /// Person credit (writer, artist, etc.) from ComicVine
    /// </summary>
    public class ComicVinePersonCreditDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? SiteDetailUrl { get; set; }
    }

    /// <summary>
    /// Simple issue reference within a volume
    /// </summary>
    public class ComicVineIssueReferenceDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string IssueNumber { get; set; } = string.Empty;
    }

    /// <summary>
    /// Request for searching ComicVine
    /// </summary>
    public class ComicVineSearchRequest
    {
        public string Query { get; set; } = string.Empty;
        public ComicVineResourceType? ResourceType { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// Types of resources that can be searched in ComicVine
    /// </summary>
    public enum ComicVineResourceType
    {
        Issue,
        Volume,
        Character,
        Publisher,
        Person,
        StoryArc,
        Team
    }

    /// <summary>
    /// Paginated search results from ComicVine
    /// </summary>
    public class ComicVineSearchResponse
    {
        public List<ComicVineSearchResultDto> Results { get; set; } = new();
        public int TotalResults { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalResults / PageSize) : 0;
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }

    /// <summary>
    /// Request to import a comic from ComicVine into user's collection
    /// </summary>
    public class ImportFromComicVineRequest
    {
        public int ComicVineIssueId { get; set; }
        
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Models.ComicCondition Condition { get; set; }
        
        public decimal? PurchasePrice { get; set; }
        public string? Notes { get; set; }
    }
}
