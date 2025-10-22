using AutoMapper;
using comicTracker.Data;
using comicTracker.DTOs;
using comicTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace comicTracker.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ComicTrackerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<WishlistService> _logger;

        public WishlistService(ComicTrackerDbContext context, IMapper mapper, ILogger<WishlistService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResult<WishlistItemDto>> GetUserWishlistAsync(int userId, WishlistQuery query)
        {
            try
            {
                var wishlistQuery = _context.WishlistItems
                    .Where(w => w.UserId == userId)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrEmpty(query.Search))
                {
                    var searchTerm = query.Search.ToLower();
                    wishlistQuery = wishlistQuery.Where(w =>
                        w.SeriesName.ToLower().Contains(searchTerm) ||
                        w.IssueNumber.ToLower().Contains(searchTerm) ||
                        (w.Publisher != null && w.Publisher.ToLower().Contains(searchTerm)));
                }

                // Apply publisher filter
                if (!string.IsNullOrEmpty(query.Publisher))
                {
                    wishlistQuery = wishlistQuery.Where(w => w.Publisher == query.Publisher);
                }

                // Apply priority filter
                if (query.Priority.HasValue)
                {
                    wishlistQuery = wishlistQuery.Where(w => w.Priority == query.Priority.Value);
                }

                // Apply condition filter
                if (query.DesiredCondition.HasValue)
                {
                    wishlistQuery = wishlistQuery.Where(w => w.DesiredCondition == query.DesiredCondition.Value);
                }

                // Apply sorting
                wishlistQuery = query.SortBy.ToLower() switch
                {
                    "seriesname" => query.SortDirection.ToLower() == "desc"
                        ? wishlistQuery.OrderByDescending(w => w.SeriesName)
                        : wishlistQuery.OrderBy(w => w.SeriesName),
                    "issuenumber" => query.SortDirection.ToLower() == "desc"
                        ? wishlistQuery.OrderByDescending(w => w.IssueNumber)
                        : wishlistQuery.OrderBy(w => w.IssueNumber),
                    "publisher" => query.SortDirection.ToLower() == "desc"
                        ? wishlistQuery.OrderByDescending(w => w.Publisher)
                        : wishlistQuery.OrderBy(w => w.Publisher),
                    "targetprice" => query.SortDirection.ToLower() == "desc"
                        ? wishlistQuery.OrderByDescending(w => w.TargetPrice)
                        : wishlistQuery.OrderBy(w => w.TargetPrice),
                    "dateadded" => query.SortDirection.ToLower() == "desc"
                        ? wishlistQuery.OrderByDescending(w => w.DateAdded)
                        : wishlistQuery.OrderBy(w => w.DateAdded),
                    _ => query.SortDirection.ToLower() == "desc"
                        ? wishlistQuery.OrderByDescending(w => w.Priority)
                        : wishlistQuery.OrderBy(w => w.Priority)
                };

                var totalCount = await wishlistQuery.CountAsync();

                var items = await wishlistQuery
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync();

                var itemDtos = _mapper.Map<List<WishlistItemDto>>(items);

                return new PagedResult<WishlistItemDto>
                {
                    Items = itemDtos,
                    TotalCount = totalCount,
                    Page = query.Page,
                    PageSize = query.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user wishlist for user {UserId}", userId);
                throw;
            }
        }

        public async Task<WishlistItemDto?> GetWishlistItemByIdAsync(int itemId, int userId)
        {
            try
            {
                var item = await _context.WishlistItems
                    .FirstOrDefaultAsync(w => w.Id == itemId && w.UserId == userId);

                return item != null ? _mapper.Map<WishlistItemDto>(item) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wishlist item {ItemId} for user {UserId}", itemId, userId);
                throw;
            }
        }

        public async Task<WishlistItemDto> CreateWishlistItemAsync(CreateWishlistItemRequest request, int userId)
        {
            try
            {
                var item = _mapper.Map<WishlistItem>(request);
                item.UserId = userId;
                item.DateAdded = DateTime.UtcNow;

                _context.WishlistItems.Add(item);
                await _context.SaveChangesAsync();

                return _mapper.Map<WishlistItemDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating wishlist item for user {UserId}", userId);
                throw;
            }
        }

        public async Task<WishlistItemDto?> UpdateWishlistItemAsync(int itemId, UpdateWishlistItemRequest request, int userId)
        {
            try
            {
                var item = await _context.WishlistItems
                    .FirstOrDefaultAsync(w => w.Id == itemId && w.UserId == userId);

                if (item == null)
                    return null;

                _mapper.Map(request, item);
                item.DateModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return _mapper.Map<WishlistItemDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating wishlist item {ItemId} for user {UserId}", itemId, userId);
                throw;
            }
        }

        public async Task<bool> DeleteWishlistItemAsync(int itemId, int userId)
        {
            try
            {
                var item = await _context.WishlistItems
                    .FirstOrDefaultAsync(w => w.Id == itemId && w.UserId == userId);

                if (item == null)
                    return false;

                _context.WishlistItems.Remove(item);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting wishlist item {ItemId} for user {UserId}", itemId, userId);
                throw;
            }
        }

        public async Task<ComicDto?> ConvertToOwnedAsync(int itemId, ConvertToOwnedRequest request, int userId)
        {
            try
            {
                var wishlistItem = await _context.WishlistItems
                    .FirstOrDefaultAsync(w => w.Id == itemId && w.UserId == userId);

                if (wishlistItem == null)
                    return null;

                // Create a new comic from the wishlist item
                var comic = new Comic
                {
                    UserId = userId,
                    SeriesName = wishlistItem.SeriesName,
                    IssueNumber = wishlistItem.IssueNumber,
                    Publisher = wishlistItem.Publisher,
                    Condition = request.ActualCondition,
                    PurchasePrice = request.ActualPrice,
                    CoverImageUrl = request.CoverImageUrl,
                    Notes = request.Notes ?? wishlistItem.Notes,
                    DateAdded = DateTime.UtcNow
                };

                _context.Comics.Add(comic);
                _context.WishlistItems.Remove(wishlistItem);
                
                await _context.SaveChangesAsync();

                return _mapper.Map<ComicDto>(comic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting wishlist item {ItemId} to owned comic for user {UserId}", itemId, userId);
                throw;
            }
        }
    }
}
