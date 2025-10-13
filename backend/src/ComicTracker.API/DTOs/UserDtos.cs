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
}