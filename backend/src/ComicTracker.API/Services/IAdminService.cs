namespace comicTracker.Services
{
    public interface IAdminService
    {
        Task<bool> EnsureAdminUserExistsAsync();
        Task<bool> CreateAdminUserAsync(string username, string email, string password);
    }
}