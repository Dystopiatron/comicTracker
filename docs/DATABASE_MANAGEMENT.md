# Database Management with SQLTools

This project uses **SQLTools** as the primary database management tool for PostgreSQL.

## 🔧 Setup

### Required Extensions
- **SQLTools** (`mtxr.sqltools`)
- **SQLTools PostgreSQL/Cockroach Driver** (`mtxr.sqltools-driver-pg`)

These are automatically recommended when you open the workspace.

### Connection Configuration
The PostgreSQL connection is pre-configured in `.vscode/settings.json`:
- **Host**: localhost
- **Port**: 5432  
- **Database**: comictracker_dev
- **Username**: groundcontrol
- **No password required** (local development)

## 🚀 Usage

### Connecting to Database
1. **Command Palette** (Cmd/Ctrl+Shift+P)
2. Type: "SQLTools: Connect"
3. Select: "ComicTracker PostgreSQL"

### Running Queries
1. Open any `.sql` file
2. Select your query text
3. **Right-click** → "Run on Active Connection"
4. Or use **Cmd/Ctrl+E, E**

### Useful Files
- `/sql/common-queries.sql` - Pre-written queries for development
- SQLTools will create session files automatically

## 📊 Common Tasks

### View All Users
```sql
SELECT "Id", "UserName", "Email", "IsAdmin" FROM "AspNetUsers";
```

### View Comics with Owners
```sql
SELECT c."SeriesName", c."IssueNumber", u."UserName" 
FROM "Comics" c 
JOIN "AspNetUsers" u ON c."UserId" = u."Id";
```

### Collection Statistics
```sql
SELECT COUNT(*) as total_comics, 
       SUM("PurchasePrice") as total_value 
FROM "Comics";
```

## 🏗️ Development Workflow
1. **Connect** to database via SQLTools
2. **Test queries** in session files
3. **Copy working queries** to your application
4. **Use common-queries.sql** for reference

## 🚫 Avoided Extensions
This project intentionally excludes other PostgreSQL extensions to maintain consistency:
- `cweijan.vscode-postgresql-client2`
- `ckolkman.vscode-postgres`
- `ms-ossdata.vscode-pgsql`

SQLTools provides everything needed for database management in a standardized way.