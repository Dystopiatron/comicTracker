# Comic Tracker

A full-stack comic book collection management application built with ASP.NET Core and React.

## Project Structure

```
comicTracker/
├── backend/              # ASP.NET Core Web API
│   ├── ComicTracker.sln  # Solution file
│   └── src/
│       └── ComicTracker.API/  # Main API project
├── frontend/             # React application
├── docs/                 # Documentation
├── README.md            # This file
└── .gitignore           # Git ignore rules
```

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Node.js 18+ and npm
- Visual Studio, VS Code, or Rider

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Run the API:
   ```bash
   dotnet run --project src/ComicTracker.API
   ```

The API will be available at `https://localhost:7xxx` with Swagger documentation at the root URL.

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install npm packages:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

The React app will be available at `http://localhost:3000`.

## Features

- **User Authentication**: JWT-based authentication with registration and login
- **Comic Management**: Full CRUD operations for comic collections
- **Advanced Search & Filtering**: Search by series, issue, publisher, or condition
- **Collection Statistics**: Comprehensive analytics including total value and breakdowns
- **Responsive Design**: Works on desktop and mobile devices

## Development

### Backend Development

The backend is organized with:
- **Controllers**: API endpoints
- **Services**: Business logic
- **Data**: Entity Framework Core context
- **Models**: Domain entities
- **DTOs**: Data transfer objects

### Frontend Development

The React frontend uses:
- **React Router**: Client-side routing
- **Context API**: State management
- **Modern hooks**: useAuth, useComics
- **Responsive CSS**: Mobile-first design

## Demo Credentials

- **Username**: `comicfan`
- **Password**: `MyComics2024`

## Documentation

Additional documentation can be found in the `docs/` directory:
- API implementation details
- Frontend development guide
- Copilot prompts and instructions

## License

This project is licensed under the MIT License.