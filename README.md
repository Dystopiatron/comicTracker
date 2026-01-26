# 📚 Comic Tracker

A full-stack web application for comic book collectors to manage their collections, track reading progress, and discover new titles through ComicVine integration.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)
![React](https://img.shields.io/badge/React-19-61DAFB?style=flat&logo=react)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-336791?style=flat&logo=postgresql)
![License](https://img.shields.io/badge/License-MIT-green.svg)

## ✨ Features

- **Collection Management** - Add, edit, and organize your comic book collection
- **ComicVine Integration** - Search and import comics directly from ComicVine's database
- **Wishlist** - Track comics you want to acquire
- **Statistics Dashboard** - Visualize your collection with charts and insights
- **User Authentication** - Secure JWT-based authentication system
- **Role-Based Access** - Admin and user roles with appropriate permissions

## 🛠️ Tech Stack

### Backend
- **Framework:** ASP.NET Core 9.0 Web API
- **Database:** PostgreSQL with Entity Framework Core 9.0
- **Authentication:** JWT Bearer tokens
- **External API:** ComicVine REST API integration

### Frontend
- **Framework:** React 19
- **Routing:** React Router v6
- **State Management:** React Context API
- **Styling:** CSS Modules

## 🚀 Quick Start

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 18+](https://nodejs.org/)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [ComicVine API Key](https://comicvine.gamespot.com/api/) (optional, for search feature)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/comicTracker.git
   cd comicTracker
   ```

2. **Set up the database**
   ```bash
   # Create a PostgreSQL database named 'comictracker'
   psql -U postgres -c "CREATE DATABASE comictracker;"
   ```

3. **Configure the backend**
   ```bash
   cd backend/src/ComicTracker.API
   
   # Copy the example configuration
   cp appsettings.Development.json.example appsettings.Development.json
   
   # Edit appsettings.Development.json with your database connection string
   # and optionally add your ComicVine API key
   ```

4. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Start the backend**
   ```bash
   dotnet run
   # API runs on https://localhost:5001
   ```

6. **Start the frontend** (new terminal)
   ```bash
   cd frontend
   npm install
   npm start
   # App runs on http://localhost:3000
   ```

## 📁 Project Structure

```
comicTracker/
├── backend/
│   └── src/
│       └── ComicTracker.API/  # Main API project
├── frontend/             # React application
├── docs/                 # Documentation
├── README.md            # This file
└── .gitignore           # Git ignore rules
```

## 🌟 **Quick Start for Instructors**

### Option 1: GitHub Codespaces (⚡ One-Click Testing)
1. Click the green **"Code"** button above
2. Select **"Codespaces"** → **"Create codespace on Base_Branch"**
3. Wait for the environment to load (2-3 minutes)
4. In the terminal, run: `./start-app.sh`
5. Open `http://localhost:3000` when prompted

### Option 2: Local Setup
**Prerequisites:** .NET 8 SDK, Node.js 18+

```bash
# Clone and setup
git clone https://github.com/Dystopiatron/comicTracker.git
cd comicTracker
./start-app.sh
```

## 📡 API Endpoints

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/auth/register` | POST | Register new user |
| `/api/auth/login` | POST | User login |
| `/api/comics` | GET | Get user's collection |
| `/api/comics` | POST | Add comic to collection |
| `/api/comics/{id}` | PUT | Update comic |
| `/api/comics/{id}` | DELETE | Remove comic |
| `/api/wishlist` | GET | Get user's wishlist |
| `/api/comicvine/search` | GET | Search ComicVine |
| `/api/statistics` | GET | Get collection statistics |

See [CONTROLLER_DOCUMENTATION.md](CONTROLLER_DOCUMENTATION.md) for complete API documentation.

## ⚙️ Configuration

### ComicVine API (Optional)

To enable the ComicVine search feature:

1. Get a free API key from [ComicVine](https://comicvine.gamespot.com/api/)
2. Add it to `appsettings.Development.json`:
   ```json
   {
     "ComicVine": {
       "ApiKey": "your-api-key-here"
     }
   }
   ```

The application works without a ComicVine API key—search features will show a friendly message indicating the feature is not configured.

## 🐳 Docker Deployment

```bash
docker-compose up -d
```

See [DEPLOYMENT.md](DEPLOYMENT.md) for detailed deployment options.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Anthony Williams**

This project is licensed under the MIT License.
