# Comic Tracker - Deployment Guide

## Project Overview
Full-stack comic collection management application with React frontend and ASP.NET Core backend.

## Tech Stack
- **Frontend**: React 19.2.0 (JavaScript), Bootstrap CSS
- **Backend**: ASP.NET Core 9.0 Web API
- **Database**: SQLite with Entity Framework Core
- **Authentication**: JWT Bearer tokens

## Local Development Setup

### Prerequisites
- Node.js 18+ 
- .NET 9.0 SDK
- Git

### Quick Start
1. Clone the repository:
   ```bash
   git clone git@github.com:Dystopiatron/comicTracker.git
   cd comicTracker
   ```

2. Run the development script:
   ```bash
   chmod +x dev-start.sh
   ./dev-start.sh
   ```

3. Access the application:
   - Frontend: http://localhost:3000
   - Backend API: http://localhost:8000

### Manual Setup

#### Backend Setup
```bash
cd backend/src/ComicTracker.API
dotnet restore
dotnet ef database update
dotnet run
```

#### Frontend Setup
```bash
cd frontend
npm install
npm start
```

## Demo Credentials
- **User 1**: comicfan / MyComics2024
- **User 2**: demouser / Demo123!
- **User 3**: Shazam / Shazam123!

## Deployment Options

### Option 1: Railway (Recommended for .NET)
Railway supports .NET applications well and can handle both frontend and backend.

1. Connect your GitHub repo to Railway
2. Deploy backend as a .NET service
3. Deploy frontend as a Node.js service
4. Set environment variables for database and JWT

### Option 2: Render
1. Backend: Deploy as Web Service (Docker or .NET)
2. Frontend: Deploy as Static Site
3. Configure environment variables

### Option 3: Azure App Service
Ideal for .NET applications:
1. Create App Service for backend (.NET 9)
2. Create Static Web App for frontend
3. Configure connection strings

### Option 4: Vercel (Frontend) + Railway (Backend)
- Vercel: Perfect for React frontend
- Railway: Good for .NET backend

## Environment Variables Needed

### Backend (.NET)
```
ASPNETCORE_ENVIRONMENT=Production
JWT_SECRET_KEY=your-super-secret-jwt-key-here
DATABASE_URL=your-database-connection-string (for production DB)
```

### Frontend (React)
```
REACT_APP_API_URL=https://your-backend-url.com
```

## Production Considerations

1. **Database**: Replace SQLite with PostgreSQL/SQL Server for production
2. **File Storage**: Use cloud storage for cover images
3. **JWT Secret**: Use strong, random secret key
4. **CORS**: Configure for production domains
5. **HTTPS**: Ensure SSL certificates are configured

## Key Features
- ✅ User authentication & registration
- ✅ Comic collection management (CRUD)
- ✅ User profile editing
- ✅ Search and filtering
- ✅ Statistics dashboard
- ✅ Responsive design
- ✅ TypeScript-free React codebase

## Project Structure
```
comicTracker/
├── frontend/          # React application
├── backend/           # ASP.NET Core API
├── docs/             # Documentation
├── dev-start.sh      # Development startup script
└── README.md         # Project documentation
```