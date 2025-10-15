-- Comic Tracker Database Schema
-- This file contains the initial database setup for PostgreSQL

-- Create database if it doesn't exist (handled by Docker)
-- CREATE DATABASE comictracker_dev;

-- Enable UUID extension for better ID generation
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Create custom types for enums
CREATE TYPE comic_condition AS ENUM ('Poor', 'Fair', 'Good', 'Fine', 'VeryFine', 'NearMint', 'Mint');

-- Note: Entity Framework will handle table creation through migrations
-- This file is for any custom PostgreSQL-specific setup

-- Create indexes for better performance (will be added after EF migration)
-- CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comics_user_id ON "Comics" ("UserId");
-- CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comics_publisher ON "Comics" ("Publisher");
-- CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comics_condition ON "Comics" ("Condition");
-- CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comics_date_added ON "Comics" ("DateAdded");

-- Create full-text search indexes for comic titles
-- CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comics_title_search ON "Comics" USING gin(to_tsvector('english', "Title"));
-- CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comics_series_search ON "Comics" USING gin(to_tsvector('english', "Series"));

-- Set up database configuration for optimal performance
ALTER SYSTEM SET shared_preload_libraries = 'pg_stat_statements';
ALTER SYSTEM SET track_activity_query_size = 2048;
ALTER SYSTEM SET pg_stat_statements.track = 'all';