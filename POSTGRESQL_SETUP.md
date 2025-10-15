# PostgreSQL Setup Instructions

## 🐘 **Database Setup Options**

### Option 1: Docker (Recommended for Development)

1. **Install Docker Desktop** (if not already installed)
2. **Start PostgreSQL and Adminer**:
   ```bash
   docker-compose up -d
   ```

3. **Access Database Tools**:
   - **Adminer (Web UI)**: http://localhost:8080
   - **Direct Connection**: localhost:5432
   - **Credentials**: 
     - Server: postgres
     - Username: postgres  
     - Password: password
     - Database: comictracker_dev

### Option 2: Local PostgreSQL Installation

1. **Install PostgreSQL** (version 13+)
2. **Create Database**:
   ```sql
   CREATE DATABASE comictracker_dev;
   CREATE USER postgres WITH PASSWORD 'password';
   GRANT ALL PRIVILEGES ON DATABASE comictracker_dev TO postgres;
   ```

## 🔧 **Entity Framework Setup**

### Generate Initial Migration:
```bash
cd backend/src/ComicTracker.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### View Generated Tables:
```sql
-- Connect to database and verify tables
\dt
-- List all tables

SELECT * FROM "AspNetUsers";
SELECT * FROM "Comics";
```

## 📊 **SQL Tools Integration**

### Popular SQL Tools that work with PostgreSQL:
- **pgAdmin** - Full-featured PostgreSQL admin tool
- **DBeaver** - Universal database tool  
- **DataGrip** - JetBrains database IDE
- **Adminer** - Lightweight web interface (included in Docker setup)
- **psql** - Command line client

### Connection Settings:
- **Host**: localhost
- **Port**: 5432
- **Database**: comictracker_dev
- **Username**: postgres
- **Password**: password

## 🎯 **Development Workflow**

1. **Start Database**: `docker-compose up -d`
2. **Run Migrations**: `dotnet ef database update`
3. **Start API**: `dotnet run`
4. **View Data**: Open Adminer at http://localhost:8080

## 🚀 **Production Deployment**

For production deployment (Railway, Render, etc.):
- The PostgreSQL connection string will be provided by the hosting service
- No Docker needed - managed PostgreSQL instance
- Environment variables will override local settings