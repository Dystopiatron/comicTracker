# PostgreSQL Management Commands

## Connect to database
```bash
psql -d comictracker_dev
```

## Common queries
```sql
-- List all tables
\dt

-- See users 
SELECT "Id", "UserName", "Email", "IsAdmin" FROM "AspNetUsers";

-- See comics
SELECT "Id", "SeriesName", "IssueNumber", "Publisher" FROM "Comics" LIMIT 10;

-- Count comics by user
SELECT u."UserName", COUNT(c."Id") as comic_count 
FROM "AspNetUsers" u 
LEFT JOIN "Comics" c ON u."Id" = c."UserId" 
GROUP BY u."Id", u."UserName";

-- Exit psql
\q
```

## Execute SQL file
```bash
psql -d comictracker_dev -f your-file.sql
```