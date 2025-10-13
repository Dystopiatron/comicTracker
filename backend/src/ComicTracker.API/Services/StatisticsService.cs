using comicTracker.Data;
using comicTracker.DTOs;
using Microsoft.EntityFrameworkCore;

namespace comicTracker.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ComicTrackerDbContext _context;
        private readonly ILogger<StatisticsService> _logger;

        public StatisticsService(ComicTrackerDbContext context, ILogger<StatisticsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserStatistics> GetUserStatisticsAsync(int userId)
        {
            try
            {
                var comics = await _context.Comics
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                var totalComics = comics.Count;
                var seriesTracked = comics.Select(c => c.SeriesName).Distinct().Count();
                var publishersCount = comics.Where(c => !string.IsNullOrEmpty(c.Publisher))
                    .Select(c => c.Publisher).Distinct().Count();
                var totalValue = comics.Where(c => c.PurchasePrice.HasValue)
                    .Sum(c => c.PurchasePrice!.Value);

                var mostValuableComic = comics
                    .Where(c => c.PurchasePrice.HasValue)
                    .OrderByDescending(c => c.PurchasePrice!.Value)
                    .FirstOrDefault();

                var conditionBreakdown = comics
                    .GroupBy(c => c.Condition.ToString())
                    .ToDictionary(g => g.Key, g => g.Count());

                var publisherBreakdown = comics
                    .Where(c => !string.IsNullOrEmpty(c.Publisher))
                    .GroupBy(c => c.Publisher!)
                    .ToDictionary(g => g.Key, g => g.Count());

                return new UserStatistics
                {
                    TotalComics = totalComics,
                    SeriesTracked = seriesTracked,
                    PublishersCount = publishersCount,
                    TotalValue = totalValue,
                    MostValuableComic = mostValuableComic != null 
                        ? $"{mostValuableComic.SeriesName} {mostValuableComic.IssueNumber}" 
                        : null,
                    ConditionBreakdown = conditionBreakdown,
                    PublisherBreakdown = publisherBreakdown
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user statistics for user {UserId}", userId);
                throw;
            }
        }

        public async Task<PublisherBreakdown> GetPublisherBreakdownAsync(int userId)
        {
            try
            {
                var publisherBreakdown = await _context.Comics
                    .Where(c => c.UserId == userId && !string.IsNullOrEmpty(c.Publisher))
                    .GroupBy(c => c.Publisher!)
                    .Select(g => new { Publisher = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.Publisher, x => x.Count);

                return new PublisherBreakdown
                {
                    Publishers = publisherBreakdown
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting publisher breakdown for user {UserId}", userId);
                throw;
            }
        }

        public async Task<ConditionBreakdown> GetConditionBreakdownAsync(int userId)
        {
            try
            {
                var conditionBreakdown = await _context.Comics
                    .Where(c => c.UserId == userId)
                    .GroupBy(c => c.Condition.ToString())
                    .Select(g => new { Condition = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.Condition, x => x.Count);

                return new ConditionBreakdown
                {
                    Conditions = conditionBreakdown
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting condition breakdown for user {UserId}", userId);
                throw;
            }
        }
    }
}