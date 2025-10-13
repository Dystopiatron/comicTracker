using comicTracker.DTOs;

namespace comicTracker.Services
{
    public interface IStatisticsService
    {
        Task<UserStatistics> GetUserStatisticsAsync(int userId);
        Task<PublisherBreakdown> GetPublisherBreakdownAsync(int userId);
        Task<ConditionBreakdown> GetConditionBreakdownAsync(int userId);
    }
}