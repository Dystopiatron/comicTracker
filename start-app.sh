#!/bin/bash

echo "🚀 Starting Comic Tracker Application..."
echo ""

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
echo ""
echo "📝 Test Accounts:"
echo "   Username: demouser    Password: Demo123!"
echo "   Username: comicfan    Password: MyComics2024"
echo ""
echo "Press Ctrl+C to stop both services"

# Wait for user to stop
wait $FRONTEND_PID $BACKEND_PID