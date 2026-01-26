# ComicVine API Integration - Implementation Guide

## Overview

This document outlines the ComicVine API integration for comicTracker, enabling users to search the ComicVine comic database and auto-populate comic metadata when adding comics to their collection.

**API Source:** [ComicVine API](https://comicvine.gamespot.com/api/) - Direct HTTP integration with Comic Vine's REST API

---

## Prerequisites

### 1. ComicVine API Key

You need an API key from ComicVine to use this integration:

1. Go to [https://comicvine.gamespot.com/api/](https://comicvine.gamespot.com/api/)
2. Create a free account or log in
3. Generate your API key
4. Add the key to your configuration (see Configuration section)

---

## Configuration

### Backend (appsettings.json)

Add your API key to the configuration:

```json
{
  "ComicVine": {
    "ApiKey": "YOUR_API_KEY_HERE",
    "UserAgent": "ComicTracker/1.0 (your-email@example.com)"
  }
}
```

**Important:** Never commit your API key to source control. Use environment variables or user secrets for production:

```bash
# Using .NET User Secrets (Development)
dotnet user-secrets set "ComicVine:ApiKey" "your-api-key-here"
```

---

## Implementation Status

### Backend ✅

| Component | File | Status |
|-----------|------|--------|
| DTOs | `DTOs/ComicVineDtos.cs` | ✅ Complete |
| Service Interface | `Services/IComicVineService.cs` | ✅ Complete |
| Service Implementation | `Services/ComicVineService.cs` | ✅ Complete |
| Controller | `Controllers/ComicVineController.cs` | ✅ Complete |
| Configuration | `appsettings.json` | ✅ Complete |
| DI Registration | `Program.cs` | ✅ Complete |
| HttpClient Setup | `Program.cs` | ✅ Complete |
| Model Update | `Models/Comic.cs` | ✅ Complete |

### Frontend ✅

| Component | File | Status |
|-----------|------|--------|
| Service | `services/comicVineService.js` | ✅ Complete |
| Search Component | `components/Comics/ComicVineSearch.jsx` | ✅ Complete |
| Styles | `components/Comics/ComicVineSearch.css` | ✅ Complete |

### Database Migration ✅

| Task | Status |
|------|--------|
| Create migration for ComicVine fields | ✅ Complete |

---

## API Endpoints

### Search Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/comicvine/search` | Search all resources |
| GET | `/api/comicvine/search/issues` | Search issues only |
| GET | `/api/comicvine/search/volumes` | Search volumes only |

**Query Parameters:**
- `query` (required) - Search term
- `resourceType` - Filter by type: Issue, Volume, Character, Publisher, Person, StoryArc, Team
- `page` - Page number (default: 1)
- `pageSize` - Results per page (default: 10, max: 100)

### Detail Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/comicvine/issue/{id}` | Get issue details |
| GET | `/api/comicvine/volume/{id}` | Get volume details |
| GET | `/api/comicvine/volume/{id}/issues` | Get issues in a volume |
| GET | `/api/comicvine/character/{id}` | Get character details |

### Import Endpoint

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/comicvine/import` | Import issue to collection |

**Request Body:**
```json
{
  "comicVineIssueId": 123456,
  "condition": "NearMint",
  "purchasePrice": 9.99,
  "notes": "First printing"
}
```

---

## Usage Examples

### Backend (C#)

```csharp
// Search for Batman issues
var results = await _comicVineService.SearchIssuesAsync("Batman", page: 1, pageSize: 10);

// Get specific issue details
var issue = await _comicVineService.GetIssueAsync(123456);

// Import to user's collection
var comic = await _comicVineService.ImportIssueToCollectionAsync(
    comicVineIssueId: 123456,
    userId: currentUserId,
    new ImportFromComicVineRequest
    {
        Condition = ComicCondition.NearMint,
        PurchasePrice = 9.99m,
        Notes = "Signed copy"
    }
);
```

### Frontend (JavaScript)

```javascript
import { searchIssues, getIssue, importToCollection } from '../services/comicVineService';

// Search for issues
const results = await searchIssues('Spider-Man', 1, 10);

// Get issue details
const issue = await getIssue(123456);

// Import to collection
const comic = await importToCollection({
  comicVineIssueId: 123456,
  condition: 'NearMint',
  purchasePrice: 9.99,
  notes: 'First printing'
});
```

---

## Resource Types

The following resource types can be searched:

| Type | Description |
|------|-------------|
| `Issue` | Individual comic issues |
| `Volume` | Comic series/volumes |
| `Character` | Comic book characters |
| `Person` | Writers, artists, etc. |
| `Team` | Teams of characters |

> **Note:** Publisher and StoryArc resource types are not currently supported as the ComicVine API returns empty results for these searches.

---

## Data Mapping

When importing a comic from ComicVine, the following fields are mapped:

| ComicVine Field | Local Field |
|-----------------|-------------|
| `volume.name` | `seriesName` |
| `issueNumber` | `issueNumber` |
| `volume.publisher.name` | `publisher` |
| `image.mediumUrl` | `coverImageUrl` |
| `id` | `comicVineId` |

User-provided fields:
- `condition` - Selected by user
- `purchasePrice` - Entered by user
- `notes` - Entered by user

---

## Future Enhancements

1. **Testing**: Add unit and integration tests for ComicVine service

2. **Caching**: Implement caching for frequently accessed data to reduce API calls

3. **Batch Import**: Allow importing multiple issues at once from a volume

---

## Troubleshooting

### Common Issues

1. **"ComicVine API key is not configured"**
   - Ensure `ComicVine:ApiKey` is set in appsettings.json or user secrets

2. **API rate limiting**
   - ComicVine limits API calls; implement caching for frequently accessed data

3. **Image URLs not working**
   - ComicVine images require proper User-Agent header to load (configured automatically)

4. **403 Forbidden errors**
   - Ensure your API key is valid
   - Check that User-Agent header is set (required by ComicVine)

---

## References

- [ComicVine API Documentation](https://comicvine.gamespot.com/api/documentation)
- [ComicVine API Registration](https://comicvine.gamespot.com/api/)
