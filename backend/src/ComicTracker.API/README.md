# ComicTracker API

ASP.NET Core 9.0 Web API for the Comic Tracker application.

## Setup

Prerequisites: .NET 9 SDK and PostgreSQL.

```bash
cd backend/src/ComicTracker.API
dotnet run
```

The HTTP profile listens on http://localhost:8000. Connection strings and the development JWT settings live in `appsettings.Development.json`.

## ComicVine API key

Search and import stay off until a key is configured. Do not commit the key.

```bash
dotnet user-secrets set "ComicVine:ApiKey" "your-api-key-here"
```

Or set `ComicVine__ApiKey` in the environment. A free key is available at https://comicvine.gamespot.com/api/.

## Migrations

```bash
dotnet ef database update
```

`20261031120000_AddComicVineReferenceToComics` adds nullable `ComicVineId` and `ComicVineLastSync` columns on `Comics`. It does not rewrite existing rows.

## Layout

- `Controllers/` — HTTP endpoints, including `ComicVineController`
- `Services/` — business logic, including `ComicVineService`
- `Data/` — Entity Framework context
- `Models/` — entities
- `DTOs/` — request and response types
- `Migrations/` — schema migrations
