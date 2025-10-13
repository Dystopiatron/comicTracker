using comicTracker.DTOs;

namespace comicTracker.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterRequest request);
        Task<AuthResult> LoginAsync(LoginRequest request);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string userId);
    }
}