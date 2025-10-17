using comicTracker.Models;

namespace comicTracker.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string? AvatarUrl { get; set; }
        public UserRole Role { get; set; }
        public string RoleDisplayName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } // Legacy support
        public int ComicCount { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }

    public class UpdateUserProfileRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
    }

    public class UserStatistics
    {
        public int TotalComics { get; set; }
        public int SeriesTracked { get; set; }
        public int PublishersCount { get; set; }
        public decimal TotalValue { get; set; }
        public string? MostValuableComic { get; set; }
        public Dictionary<string, int> ConditionBreakdown { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> PublisherBreakdown { get; set; } = new Dictionary<string, int>();
    }

    public class PublisherBreakdown
    {
        public Dictionary<string, int> Publishers { get; set; } = new Dictionary<string, int>();
    }

    public class ConditionBreakdown
    {
        public Dictionary<string, int> Conditions { get; set; } = new Dictionary<string, int>();
    }

    public class AdminUserUpdateRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class UserWithComicsDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string? AvatarUrl { get; set; }
        public UserRole Role { get; set; }
        public string RoleDisplayName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } // Legacy support
        public List<ComicDto> Comics { get; set; } = new List<ComicDto>();
    }

    public class PromoteUserRequest
    {
        public UserRole NewRole { get; set; }
        public string? Reason { get; set; }
    }

    public class RoleInfoDto
    {
        public UserRole Role { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}