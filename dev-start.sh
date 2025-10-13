#!/bin/bash

# Comic Tracker Development Scripts
# Usage: ./dev-start.sh [backend|frontend|both]

case "$1" in
    "backend")
        echo "Starting backend API..."
        cd backend && dotnet run --project src/ComicTracker.API
        ;;
    "frontend")
        echo "Starting frontend React app..."
        cd frontend && npm start
        ;;
    "both")
        echo "Starting both backend and frontend..."
        echo "Starting backend API in background..."
        cd backend && dotnet run --project src/ComicTracker.API &
        BACKEND_PID=$!
        echo "Backend PID: $BACKEND_PID"
        
        sleep 5
        echo "Starting frontend..."
        cd ../frontend && npm start &
        FRONTEND_PID=$!
        echo "Frontend PID: $FRONTEND_PID"
        
        echo "Both services started. Press Ctrl+C to stop both."
        trap "kill $BACKEND_PID $FRONTEND_PID" EXIT
        wait
        ;;
    *)
        echo "Usage: $0 [backend|frontend|both]"
        echo "  backend  - Start only the ASP.NET Core API"
        echo "  frontend - Start only the React development server"
        echo "  both     - Start both services"
        exit 1
        ;;
esac