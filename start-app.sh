#!/bin/bash

echo "🚀 Starting Comic Tracker Application with PostgreSQL..."
echo ""

# Start PostgreSQL if not running
echo "🐘 Starting PostgreSQL Database..."
if ! docker ps | grep -q comictracker_postgres; then
    docker-compose up -d
    echo "⏳ Waiting for PostgreSQL to be ready..."
    sleep 10
fi

# Start backend in background
echo "📡 Starting Backend API..."
cd backend/src/ComicTracker.API
dotnet run &
BACKEND_PID=$!

# Wait a moment for backend to start
sleep 5

# Start frontend
echo "🎨 Starting Frontend..."
cd ../../../frontend
npm start &
FRONTEND_PID=$!

echo ""
echo "✅ Comic Tracker is starting up!"
echo "📡 Backend API: http://localhost:8000"
echo "🎨 Frontend: http://localhost:3000"
echo "🐘 PostgreSQL: localhost:5432"
echo "🗄️ Adminer (DB UI): http://localhost:8080"
echo ""
echo "📝 Test Accounts:"
echo "   Username: demouser    Password: Demo123!"
echo "   Username: comicfan    Password: MyComics2024"
echo ""
echo "🔧 Database Access:"
echo "   Server: postgres, User: postgres, Password: password"
echo ""
echo "Press Ctrl+C to stop all services"

# Wait for user to stop
wait $FRONTEND_PID $BACKEND_PID