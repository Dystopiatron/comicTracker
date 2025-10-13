using comicTracker.DTOs;

namespace comicTracker.Services
{
    public interface IComicService
    {
        Task<PagedResult<ComicDto>> GetUserComicsAsync(int userId, ComicQuery query);
        Task<ComicDto?> GetComicByIdAsync(int comicId, int userId);
        Task<ComicDto> CreateComicAsync(CreateComicRequest request, int userId);
        Task<ComicDto?> UpdateComicAsync(int comicId, UpdateComicRequest request, int userId);
        Task<bool> DeleteComicAsync(int comicId, int userId);
        Task<List<ComicDto>> SearchComicsAsync(string searchTerm, int userId);
    }
}