using comicTracker.DTOs;

namespace comicTracker.Services
{
    /// <summary>
    /// Service interface for interacting with the ComicVine API
    /// </summary>
    public interface IComicVineService
    {
        /// <summary>
        /// Indicates whether the ComicVine API is configured with a valid API key
        /// </summary>
        bool IsConfigured { get; }

        /// <summary>
        /// Search ComicVine for comics, characters, volumes, etc.
        /// </summary>
        Task<ComicVineSearchResponse> SearchAsync(ComicVineSearchRequest request);

        /// <summary>
        /// Get detailed information about a specific issue
        /// </summary>
        Task<ComicVineIssueDto?> GetIssueAsync(int issueId);

        /// <summary>
        /// Get detailed information about a volume (comic series)
        /// </summary>
        Task<ComicVineVolumeDto?> GetVolumeAsync(int volumeId);

        /// <summary>
        /// Get issues for a specific volume
        /// </summary>
        Task<List<ComicVineIssueReferenceDto>> GetVolumeIssuesAsync(int volumeId);

        /// <summary>
        /// Get detailed information about a character
        /// </summary>
        Task<ComicVineCharacterDto?> GetCharacterAsync(int characterId);

        /// <summary>
        /// Search specifically for issues
        /// </summary>
        Task<ComicVineSearchResponse> SearchIssuesAsync(string query, int page = 1, int pageSize = 10);

        /// <summary>
        /// Search specifically for volumes (series)
        /// </summary>
        Task<ComicVineSearchResponse> SearchVolumesAsync(string query, int page = 1, int pageSize = 10);

        /// <summary>
        /// Import an issue from ComicVine into user's collection
        /// </summary>
        Task<ComicDto> ImportIssueToCollectionAsync(int comicVineIssueId, int userId, ImportFromComicVineRequest request);
    }
}
