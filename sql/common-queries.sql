-- Comic Tracker Database Queries
-- Use these queries with SQLTools for development and debugging

-- ==============================================
-- USER MANAGEMENT QUERIES
-- ==============================================

-- Get all users with admin status
SELECT "Id", "UserName", "Email", "IsAdmin", "DateCreated" 
FROM "AspNetUsers" 
ORDER BY "DateCreated" DESC;

-- Get user with their comic count
SELECT 
    u."Id", 
    u."UserName", 
    u."Email", 
    u."IsAdmin",
    COUNT(c."Id") as "ComicCount",
    COALESCE(SUM(c."PurchasePrice"), 0) as "TotalValue"
FROM "AspNetUsers" u 
LEFT JOIN "Comics" c ON u."Id" = c."UserId" 
GROUP BY u."Id", u."UserName", u."Email", u."IsAdmin"
ORDER BY "ComicCount" DESC;

-- ==============================================
-- COMIC QUERIES
-- ==============================================

-- Get all comics with user info
SELECT 
    c."Id",
    c."SeriesName",
    c."IssueNumber",
    c."Publisher",
    c."Condition",
    c."PurchasePrice",
    u."UserName" as "Owner",
    c."DateAdded"
FROM "Comics" c
JOIN "AspNetUsers" u ON c."UserId" = u."Id"
ORDER BY c."DateAdded" DESC
LIMIT 20;

-- Comics by publisher
SELECT 
    "Publisher",
    COUNT(*) as "Count",
    AVG("PurchasePrice") as "AvgPrice",
    SUM("PurchasePrice") as "TotalValue"
FROM "Comics" 
WHERE "Publisher" IS NOT NULL
GROUP BY "Publisher"
ORDER BY "Count" DESC;

-- Most expensive comics
SELECT 
    "SeriesName",
    "IssueNumber",
    "Publisher",
    "PurchasePrice",
    u."UserName" as "Owner"
FROM "Comics" c
JOIN "AspNetUsers" u ON c."UserId" = u."Id"
WHERE "PurchasePrice" IS NOT NULL
ORDER BY "PurchasePrice" DESC
LIMIT 10;

-- ==============================================
-- STATISTICS QUERIES
-- ==============================================

-- Database overview
SELECT 
    'Total Users' as "Metric", 
    COUNT(*)::text as "Value" 
FROM "AspNetUsers"
UNION ALL
SELECT 
    'Total Comics' as "Metric", 
    COUNT(*)::text as "Value" 
FROM "Comics"
UNION ALL
SELECT 
    'Total Collection Value' as "Metric", 
    '$' || COALESCE(SUM("PurchasePrice"), 0)::text as "Value" 
FROM "Comics";

-- Comics added by month
SELECT 
    DATE_TRUNC('month', "DateAdded") as "Month",
    COUNT(*) as "Comics Added"
FROM "Comics"
GROUP BY DATE_TRUNC('month', "DateAdded")
ORDER BY "Month" DESC;

-- ==============================================
-- DEVELOPMENT/DEBUG QUERIES
-- ==============================================

-- Check for orphaned comics (shouldn't exist with FK constraint)
SELECT c.* 
FROM "Comics" c 
LEFT JOIN "AspNetUsers" u ON c."UserId" = u."Id" 
WHERE u."Id" IS NULL;

-- Check data integrity
SELECT 
    COUNT(*) as "Total Comics",
    COUNT("PurchasePrice") as "Comics with Price",
    COUNT("CoverImageUrl") as "Comics with Images",
    COUNT("Notes") as "Comics with Notes"
FROM "Comics";