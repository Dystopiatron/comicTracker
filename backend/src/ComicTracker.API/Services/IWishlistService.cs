using comicTracker.DTOs;

namespace comicTracker.Services
{
    public interface IWishlistService
    {
        Task<PagedResult<WishlistItemDto>> GetUserWishlistAsync(int userId, WishlistQuery query);
        Task<WishlistItemDto?> GetWishlistItemByIdAsync(int itemId, int userId);
        Task<WishlistItemDto> CreateWishlistItemAsync(CreateWishlistItemRequest request, int userId);
        Task<WishlistItemDto?> UpdateWishlistItemAsync(int itemId, UpdateWishlistItemRequest request, int userId);
        Task<bool> DeleteWishlistItemAsync(int itemId, int userId);
        Task<ComicDto?> ConvertToOwnedAsync(int itemId, ConvertToOwnedRequest request, int userId);
    }
}
