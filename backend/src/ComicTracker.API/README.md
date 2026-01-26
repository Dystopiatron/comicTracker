# ComicTracker API

ASP.NET Core 9.0 Web API backend for the Comic Tracker application.

## Overview

This API provides RESTful endpoints for managing comic book collections, user authentication, wishlists, and ComicVine integration.

## Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)

### Setup

1. **Configure the database connection**
   ```bash
   cp appsettings.Development.json.example appsettings.Development.json
   # Edit the ConnectionStrings:DefaultConnection value
   ```

2. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

3. **Run the API**
   ```bash
   dotnet run
   ```
   
   The API will be available at `https://localhost:5001`

## Project Structure

```
ComicTracker.API/
├── Controllers/         # API endpoint controllers
│   ├── AuthController.cs         # Authentication (login/register)
│   ├── ComicsController.cs       # Collection management
│   ├── ComicVineController.cs    # ComicVine search/import
│   ├── WishlistController.cs     # Wishlist management
│   ├── StatisticsController.cs   # Collection analytics
│   ├── UsersController.cs        # User profile management
│   ├── RoleController.cs         # Role management
│   └── AdminController.cs        # Admin operations
├── Data/                # Database context
├── DTOs/                # Data transfer objects
├── Middleware/          # Custom middleware (exception handling)
├── Migrations/          # EF Core database migrations
├── Models/              # Entity models
├── Profiles/            # AutoMapper mapping profiles
└── Services/            # Business logic services
```

## Key Features

- **JWT Authentication** - Secure token-based auth with role support
- **Entity Framework Core** - Code-first database with migrations
- **AutoMapper** - DTO/Entity mapping
- **ComicVine Integration** - External API for comic metadata
- **Global Exception Handling** - Consistent error responses

## Configuration

Key settings in `appsettings.json`:

| Setting | Description |
|---------|-------------|
| `ConnectionStrings:DefaultConnection` | PostgreSQL connection string |
| `Jwt:Key` | Secret key for JWT signing |
| `Jwt:Issuer` | JWT issuer claim |
| `Jwt:Audience` | JWT audience claim |
| `ComicVine:ApiKey` | ComicVine API key (optional) |

## API Documentation

See [CONTROLLER_DOCUMENTATION.md](../../../CONTROLLER_DOCUMENTATION.md) for complete endpoint documentation.
