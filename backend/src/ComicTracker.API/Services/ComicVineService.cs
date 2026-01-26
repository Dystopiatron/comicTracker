using System.Text.Json;
using System.Text.Json.Serialization;
using comicTracker.Data;
using comicTracker.DTOs;
using comicTracker.Models;
using Microsoft.Extensions.Options;

namespace comicTracker.Services
{
    /// <summary>
    /// Service for interacting with the ComicVine API
    /// </summary>
    public class ComicVineService : IComicVineService
    {
        private const string BaseUrl = "https://comicvine.gamespot.com/api";
        
        private readonly HttpClient _httpClient;
        private readonly ComicTrackerDbContext _context;
        private readonly ILogger<ComicVineService> _logger;
        private readonly string? _apiKey;
        private readonly bool _isConfigured;
        private readonly JsonSerializerOptions _jsonOptions;

        public ComicVineService(
            IOptions<ComicVineSettings> settings,
            ComicTrackerDbContext context,
            ILogger<ComicVineService> logger,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _apiKey = settings.Value.ApiKey;
            _isConfigured = !string.IsNullOrEmpty(_apiKey);
            
            if (!_isConfigured)
            {
                _logger.LogWarning("ComicVine API key is not configured. ComicVine features will be disabled. Get a free API key at https://comicvine.gamespot.com/api/");
            }
            
            _httpClient = httpClientFactory.CreateClient("ComicVine");
            
            var userAgent = settings.Value.UserAgent ?? "ComicTracker/1.0";
            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            }
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Checks if the ComicVine API is configured with a valid API key
        /// </summary>
        public bool IsConfigured => _isConfigured;

        private void EnsureConfigured()
        {
            if (!_isConfigured)
            {
                throw new InvalidOperationException(
                    "ComicVine API is not configured. To enable this feature, get a free API key at https://comicvine.gamespot.com/api/ " +
                    "and add it to appsettings.Development.json under ComicVine:ApiKey");
            }
        }

        public async Task<ComicVineSearchResponse> SearchAsync(ComicVineSearchRequest request)
        {
            EnsureConfigured();
            
            try
            {
                _logger.LogInformation("Searching ComicVine for: {Query}", request.Query);

                var resourceFilter = request.ResourceType.HasValue 
                    ? GetResourceTypeName(request.ResourceType.Value) 
                    : null;

                var offset = (request.Page - 1) * request.PageSize;
                
                var url = $"{BaseUrl}/search/?api_key={_apiKey}&format=json&query={Uri.EscapeDataString(request.Query)}&offset={offset}&limit={request.PageSize}";
                
                if (!string.IsNullOrEmpty(resourceFilter))
                {
                    url += $"&resources={resourceFilter}";
                }

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ComicVineApiResponse<List<ComicVineApiResult>>>(json, _jsonOptions);

                if (apiResponse == null || apiResponse.StatusCode != 1)
                {
                    throw new Exception($"ComicVine API error: {apiResponse?.Error ?? "Unknown error"}");
                }

                return new ComicVineSearchResponse
                {
                    Results = apiResponse.Results?.Select(MapToSearchResult).ToList() ?? new List<ComicVineSearchResultDto>(),
                    TotalResults = apiResponse.NumberOfTotalResults,
                    Page = request.Page,
                    PageSize = request.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching ComicVine for: {Query}", request.Query);
                throw;
            }
        }

        public async Task<ComicVineSearchResponse> SearchIssuesAsync(string query, int page = 1, int pageSize = 10)
        {
            return await SearchAsync(new ComicVineSearchRequest
            {
                Query = query,
                ResourceType = ComicVineResourceType.Issue,
                Page = page,
                PageSize = pageSize
            });
        }

        public async Task<ComicVineSearchResponse> SearchVolumesAsync(string query, int page = 1, int pageSize = 10)
        {
            return await SearchAsync(new ComicVineSearchRequest
            {
                Query = query,
                ResourceType = ComicVineResourceType.Volume,
                Page = page,
                PageSize = pageSize
            });
        }

        public async Task<ComicVineIssueDto?> GetIssueAsync(int issueId)
        {
            EnsureConfigured();
            
            try
            {
                _logger.LogInformation("Getting ComicVine issue: {IssueId}", issueId);
                
                var url = $"{BaseUrl}/issue/4000-{issueId}/?api_key={_apiKey}&format=json";
                var response = await _httpClient.GetAsync(url);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ComicVineApiResponse<ComicVineApiIssue>>(json, _jsonOptions);

                if (apiResponse == null || apiResponse.StatusCode != 1 || apiResponse.Results == null)
                {
                    return null;
                }

                return MapToIssueDto(apiResponse.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ComicVine issue: {IssueId}", issueId);
                throw;
            }
        }

        public async Task<ComicVineVolumeDto?> GetVolumeAsync(int volumeId)
        {
            EnsureConfigured();
            
            try
            {
                _logger.LogInformation("Getting ComicVine volume: {VolumeId}", volumeId);
                
                var url = $"{BaseUrl}/volume/4050-{volumeId}/?api_key={_apiKey}&format=json";
                var response = await _httpClient.GetAsync(url);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ComicVineApiResponse<ComicVineApiVolume>>(json, _jsonOptions);

                if (apiResponse == null || apiResponse.StatusCode != 1 || apiResponse.Results == null)
                {
                    return null;
                }

                return MapToVolumeDto(apiResponse.Results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ComicVine volume: {VolumeId}", volumeId);
                throw;
            }
        }

        public async Task<List<ComicVineIssueReferenceDto>> GetVolumeIssuesAsync(int volumeId)
        {
            try
            {
                var volume = await GetVolumeAsync(volumeId);
                return volume?.Issues ?? new List<ComicVineIssueReferenceDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting issues for volume: {VolumeId}", volumeId);
                throw;
            }
        }

        public async Task<ComicVineCharacterDto?> GetCharacterAsync(int characterId)
        {
            EnsureConfigured();
            
            try
            {
                _logger.LogInformation("Getting ComicVine character: {CharacterId}", characterId);
                
                var url = $"{BaseUrl}/character/4005-{characterId}/?api_key={_apiKey}&format=json";
                var response = await _httpClient.GetAsync(url);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ComicVineApiResponse<ComicVineApiCharacter>>(json, _jsonOptions);

                if (apiResponse == null || apiResponse.StatusCode != 1 || apiResponse.Results == null)
                {
                    return null;
                }

                var character = apiResponse.Results;
                return new ComicVineCharacterDto
                {
                    Id = character.Id,
                    Name = character.Name ?? string.Empty,
                    RealName = character.RealName,
                    Description = StripHtml(character.Description),
                    ImageUrl = character.Image?.MediumUrl,
                    CountOfIssueAppearances = character.CountOfIssueAppearances,
                    SiteDetailUrl = character.SiteDetailUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ComicVine character: {CharacterId}", characterId);
                throw;
            }
        }

        public async Task<ComicDto> ImportIssueToCollectionAsync(int comicVineIssueId, int userId, ImportFromComicVineRequest request)
        {
            _logger.LogInformation("Importing ComicVine issue {IssueId} to user {UserId}'s collection", comicVineIssueId, userId);

            // Get issue details from ComicVine
            var issue = await GetIssueAsync(comicVineIssueId);
            if (issue == null)
            {
                throw new ArgumentException($"ComicVine issue with ID {comicVineIssueId} not found");
            }

            // Create the comic in user's collection
            var comic = new Comic
            {
                SeriesName = issue.Volume?.Name ?? "Unknown Series",
                IssueNumber = issue.IssueNumber ?? "#1",
                Publisher = issue.Volume?.Publisher?.Name,
                Condition = request.Condition,
                PurchasePrice = request.PurchasePrice,
                CoverImageUrl = issue.CoverImageUrl,
                Notes = request.Notes,
                UserId = userId,
                DateAdded = DateTime.UtcNow,
                ComicVineId = comicVineIssueId,
                ComicVineLastSync = DateTime.UtcNow
            };

            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully imported ComicVine issue {IssueId} as comic {ComicId}", comicVineIssueId, comic.Id);

            return new ComicDto
            {
                Id = comic.Id,
                SeriesName = comic.SeriesName,
                IssueNumber = comic.IssueNumber,
                Publisher = comic.Publisher,
                Condition = comic.Condition.ToString(),
                PurchasePrice = comic.PurchasePrice,
                CoverImageUrl = comic.CoverImageUrl,
                Notes = comic.Notes,
                DateAdded = comic.DateAdded,
                DateModified = comic.DateModified
            };
        }

        #region Private Helper Methods

        private static string GetResourceTypeName(ComicVineResourceType resourceType)
        {
            return resourceType switch
            {
                ComicVineResourceType.Issue => "issue",
                ComicVineResourceType.Volume => "volume",
                ComicVineResourceType.Character => "character",
                ComicVineResourceType.Publisher => "publisher",
                ComicVineResourceType.Person => "person",
                ComicVineResourceType.StoryArc => "story_arc",
                ComicVineResourceType.Team => "team",
                _ => throw new ArgumentOutOfRangeException(nameof(resourceType))
            };
        }

        private static ComicVineSearchResultDto MapToSearchResult(ComicVineApiResult result)
        {
            // Build a display name - for issues, use "Volume Name #IssueNumber" format
            string displayName;
            if (result.ResourceType == "issue" && result.Volume != null)
            {
                var volumeName = result.Volume.Name ?? "Unknown Series";
                var issueNum = result.IssueNumber ?? "?";
                var issueName = !string.IsNullOrEmpty(result.Name) ? $" - {result.Name}" : "";
                displayName = $"{volumeName} #{issueNum}{issueName}";
            }
            else
            {
                displayName = result.Name ?? "Unknown";
            }
            
            return new ComicVineSearchResultDto
            {
                Id = result.Id,
                Name = displayName,
                Description = StripHtml(result.Deck ?? result.Description),
                ImageUrl = result.Image?.MediumUrl ?? result.Image?.SmallUrl,
                ResourceType = result.ResourceType ?? "unknown",
                SiteDetailUrl = result.SiteDetailUrl
            };
        }

        private static ComicVineIssueDto MapToIssueDto(ComicVineApiIssue issue)
        {
            return new ComicVineIssueDto
            {
                Id = issue.Id,
                Name = issue.Name,
                IssueNumber = issue.IssueNumber ?? string.Empty,
                Description = StripHtml(issue.Description),
                CoverImageUrl = issue.Image?.MediumUrl,
                SmallImageUrl = issue.Image?.SmallUrl,
                CoverDate = ParseDate(issue.CoverDate),
                StoreDate = ParseDate(issue.StoreDate),
                Volume = issue.Volume != null ? new ComicVineVolumeDto
                {
                    Id = issue.Volume.Id,
                    Name = issue.Volume.Name ?? string.Empty,
                    Publisher = issue.Volume.Publisher != null ? new ComicVinePublisherDto
                    {
                        Id = issue.Volume.Publisher.Id,
                        Name = issue.Volume.Publisher.Name ?? string.Empty
                    } : null
                } : null,
                PersonCredits = issue.PersonCredits?
                    .Select(p => new ComicVinePersonCreditDto
                    {
                        Id = p.Id,
                        Name = p.Name ?? string.Empty,
                        Role = p.Role,
                        SiteDetailUrl = p.SiteDetailUrl
                    })
                    .ToList() ?? new List<ComicVinePersonCreditDto>(),
                Characters = issue.CharacterCredits?
                    .Select(c => new ComicVineCharacterDto
                    {
                        Id = c.Id,
                        Name = c.Name ?? string.Empty
                    })
                    .ToList() ?? new List<ComicVineCharacterDto>(),
                SiteDetailUrl = issue.SiteDetailUrl
            };
        }

        private static ComicVineVolumeDto MapToVolumeDto(ComicVineApiVolume volume)
        {
            return new ComicVineVolumeDto
            {
                Id = volume.Id,
                Name = volume.Name ?? string.Empty,
                Description = StripHtml(volume.Description),
                StartYear = volume.StartYear,
                CountOfIssues = volume.CountOfIssues,
                ImageUrl = volume.Image?.MediumUrl,
                Publisher = volume.Publisher != null ? new ComicVinePublisherDto
                {
                    Id = volume.Publisher.Id,
                    Name = volume.Publisher.Name ?? string.Empty
                } : null,
                Issues = volume.Issues?
                    .Select(i => new ComicVineIssueReferenceDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        IssueNumber = i.IssueNumber ?? string.Empty
                    })
                    .ToList() ?? new List<ComicVineIssueReferenceDto>(),
                SiteDetailUrl = volume.SiteDetailUrl
            };
        }

        private static DateTime? ParseDate(string? dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
                return null;
            
            if (DateTime.TryParse(dateStr, out var date))
                return date;
            
            return null;
        }

        private static string? StripHtml(string? html)
        {
            if (string.IsNullOrEmpty(html))
                return null;

            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty).Trim();
        }

        #endregion
    }

    #region ComicVine API Response Models

    internal class ComicVineApiResponse<T>
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        
        [JsonPropertyName("number_of_page_results")]
        public int NumberOfPageResults { get; set; }
        
        [JsonPropertyName("number_of_total_results")]
        public int NumberOfTotalResults { get; set; }
        
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }
        
        [JsonPropertyName("results")]
        public T? Results { get; set; }
    }

    internal class ComicVineApiResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("deck")]
        public string? Deck { get; set; }
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("resource_type")]
        public string? ResourceType { get; set; }
        
        [JsonPropertyName("site_detail_url")]
        public string? SiteDetailUrl { get; set; }
        
        [JsonPropertyName("image")]
        public ComicVineApiImage? Image { get; set; }
        
        // Issue-specific fields (included in search results for issues)
        [JsonPropertyName("issue_number")]
        public string? IssueNumber { get; set; }
        
        [JsonPropertyName("volume")]
        public ComicVineApiVolumeRef? Volume { get; set; }
    }

    internal class ComicVineApiImage
    {
        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }
        
        [JsonPropertyName("medium_url")]
        public string? MediumUrl { get; set; }
        
        [JsonPropertyName("small_url")]
        public string? SmallUrl { get; set; }
        
        [JsonPropertyName("thumb_url")]
        public string? ThumbUrl { get; set; }
        
        [JsonPropertyName("original_url")]
        public string? OriginalUrl { get; set; }
    }

    internal class ComicVineApiIssue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("issue_number")]
        public string? IssueNumber { get; set; }
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("cover_date")]
        public string? CoverDate { get; set; }
        
        [JsonPropertyName("store_date")]
        public string? StoreDate { get; set; }
        
        [JsonPropertyName("site_detail_url")]
        public string? SiteDetailUrl { get; set; }
        
        [JsonPropertyName("image")]
        public ComicVineApiImage? Image { get; set; }
        
        [JsonPropertyName("volume")]
        public ComicVineApiVolumeRef? Volume { get; set; }
        
        [JsonPropertyName("person_credits")]
        public List<ComicVineApiPersonCredit>? PersonCredits { get; set; }
        
        [JsonPropertyName("character_credits")]
        public List<ComicVineApiCharacterRef>? CharacterCredits { get; set; }
    }

    internal class ComicVineApiVolume
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("start_year")]
        public int? StartYear { get; set; }
        
        [JsonPropertyName("count_of_issues")]
        public int? CountOfIssues { get; set; }
        
        [JsonPropertyName("site_detail_url")]
        public string? SiteDetailUrl { get; set; }
        
        [JsonPropertyName("image")]
        public ComicVineApiImage? Image { get; set; }
        
        [JsonPropertyName("publisher")]
        public ComicVineApiPublisherRef? Publisher { get; set; }
        
        [JsonPropertyName("issues")]
        public List<ComicVineApiIssueRef>? Issues { get; set; }
    }

    internal class ComicVineApiVolumeRef
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("site_detail_url")]
        public string? SiteDetailUrl { get; set; }
        
        [JsonPropertyName("publisher")]
        public ComicVineApiPublisherRef? Publisher { get; set; }
    }

    internal class ComicVineApiPublisherRef
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    internal class ComicVineApiIssueRef
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("issue_number")]
        public string? IssueNumber { get; set; }
    }

    internal class ComicVineApiPersonCredit
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("role")]
        public string? Role { get; set; }
        
        [JsonPropertyName("site_detail_url")]
        public string? SiteDetailUrl { get; set; }
    }

    internal class ComicVineApiCharacterRef
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    internal class ComicVineApiCharacter
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("real_name")]
        public string? RealName { get; set; }
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("count_of_issue_appearances")]
        public int? CountOfIssueAppearances { get; set; }
        
        [JsonPropertyName("site_detail_url")]
        public string? SiteDetailUrl { get; set; }
        
        [JsonPropertyName("image")]
        public ComicVineApiImage? Image { get; set; }
    }

    #endregion

    /// <summary>
    /// Configuration settings for ComicVine API
    /// </summary>
    public class ComicVineSettings
    {
        public const string SectionName = "ComicVine";
        
        /// <summary>
        /// API key from https://comicvine.gamespot.com/api
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        
        /// <summary>
        /// Custom User-Agent header for API requests
        /// </summary>
        public string? UserAgent { get; set; }
    }
}
