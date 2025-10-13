using AutoMapper;
using comicTracker.Data;
using comicTracker.DTOs;
using comicTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace comicTracker.Services
{
    public class ComicService : IComicService
    {
        private readonly ComicTrackerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ComicService> _logger;

        public ComicService(ComicTrackerDbContext context, IMapper mapper, ILogger<ComicService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResult<ComicDto>> GetUserComicsAsync(int userId, ComicQuery query)
        {
            try
            {
                var comicsQuery = _context.Comics
                    .Where(c => c.UserId == userId)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrEmpty(query.Search))
                {
                    var searchTerm = query.Search.ToLower();
                    comicsQuery = comicsQuery.Where(c => 
                        c.SeriesName.ToLower().Contains(searchTerm) ||
                        c.IssueNumber.ToLower().Contains(searchTerm) ||
                        (c.Publisher != null && c.Publisher.ToLower().Contains(searchTerm)));
                }

                // Apply publisher filter
                if (!string.IsNullOrEmpty(query.Publisher))
                {
                    comicsQuery = comicsQuery.Where(c => c.Publisher == query.Publisher);
                }

                // Apply condition filter
                if (query.Condition.HasValue)
                {
                    comicsQuery = comicsQuery.Where(c => c.Condition == query.Condition.Value);
                }

                // Apply sorting
                comicsQuery = query.SortBy.ToLower() switch
                {
                    "seriesname" => query.SortDirection.ToLower() == "desc" 
                        ? comicsQuery.OrderByDescending(c => c.SeriesName)
                        : comicsQuery.OrderBy(c => c.SeriesName),
                    "issuenumber" => query.SortDirection.ToLower() == "desc" 
                        ? comicsQuery.OrderByDescending(c => c.IssueNumber)
                        : comicsQuery.OrderBy(c => c.IssueNumber),
                    "publisher" => query.SortDirection.ToLower() == "desc" 
                        ? comicsQuery.OrderByDescending(c => c.Publisher)
                        : comicsQuery.OrderBy(c => c.Publisher),
                    "condition" => query.SortDirection.ToLower() == "desc" 
                        ? comicsQuery.OrderByDescending(c => c.Condition)
                        : comicsQuery.OrderBy(c => c.Condition),
                    "purchaseprice" => query.SortDirection.ToLower() == "desc" 
                        ? comicsQuery.OrderByDescending(c => c.PurchasePrice)
                        : comicsQuery.OrderBy(c => c.PurchasePrice),
                    _ => query.SortDirection.ToLower() == "desc" 
                        ? comicsQuery.OrderByDescending(c => c.DateAdded)
                        : comicsQuery.OrderBy(c => c.DateAdded)
                };

                var totalCount = await comicsQuery.CountAsync();
                
                var comics = await comicsQuery
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync();

                var comicDtos = _mapper.Map<List<ComicDto>>(comics);

                return new PagedResult<ComicDto>
                {
                    Items = comicDtos,
                    TotalCount = totalCount,
                    Page = query.Page,
                    PageSize = query.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user comics for user {UserId}", userId);
                throw;
            }
        }

        public async Task<ComicDto?> GetComicByIdAsync(int comicId, int userId)
        {
            try
            {
                var comic = await _context.Comics
                    .FirstOrDefaultAsync(c => c.Id == comicId && c.UserId == userId);

                return comic != null ? _mapper.Map<ComicDto>(comic) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting comic {ComicId} for user {UserId}", comicId, userId);
                throw;
            }
        }

        public async Task<ComicDto> CreateComicAsync(CreateComicRequest request, int userId)
        {
            try
            {
                var comic = _mapper.Map<Comic>(request);
                comic.UserId = userId;
                comic.DateAdded = DateTime.UtcNow;

                _context.Comics.Add(comic);
                await _context.SaveChangesAsync();

                return _mapper.Map<ComicDto>(comic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating comic for user {UserId}", userId);
                throw;
            }
        }

        public async Task<ComicDto?> UpdateComicAsync(int comicId, UpdateComicRequest request, int userId)
        {
            try
            {
                var comic = await _context.Comics
                    .FirstOrDefaultAsync(c => c.Id == comicId && c.UserId == userId);

                if (comic == null)
                    return null;

                _mapper.Map(request, comic);
                comic.DateModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return _mapper.Map<ComicDto>(comic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comic {ComicId} for user {UserId}", comicId, userId);
                throw;
            }
        }

        public async Task<bool> DeleteComicAsync(int comicId, int userId)
        {
            try
            {
                var comic = await _context.Comics
                    .FirstOrDefaultAsync(c => c.Id == comicId && c.UserId == userId);

                if (comic == null)
                    return false;

                _context.Comics.Remove(comic);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comic {ComicId} for user {UserId}", comicId, userId);
                throw;
            }
        }

        public async Task<List<ComicDto>> SearchComicsAsync(string searchTerm, int userId)
        {
            try
            {
                var comics = await _context.Comics
                    .Where(c => c.UserId == userId && 
                        (c.SeriesName.Contains(searchTerm) ||
                         c.IssueNumber.Contains(searchTerm) ||
                         (c.Publisher != null && c.Publisher.Contains(searchTerm))))
                    .OrderBy(c => c.SeriesName)
                    .ThenBy(c => c.IssueNumber)
                    .ToListAsync();

                return _mapper.Map<List<ComicDto>>(comics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching comics for user {UserId} with term {SearchTerm}", userId, searchTerm);
                throw;
            }
        }
    }
}