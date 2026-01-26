# ComicTracker Frontend

React 19 frontend application for the Comic Tracker project.

## Overview

A modern, responsive web interface for managing comic book collections with features including collection browsing, ComicVine search integration, wishlists, and statistics visualization.

## Getting Started

### Prerequisites
- [Node.js 18+](https://nodejs.org/)
- Backend API running (see backend README)

### Setup

1. **Install dependencies**
   ```bash
   npm install
   ```

2. **Start development server**
   ```bash
   npm start
   ```
   
   The app will be available at `http://localhost:3000`

## Project Structure

```
src/
├── components/          # Reusable UI components
│   ├── Admin/          # Admin dashboard components
│   ├── Auth/           # Login/register forms
│   ├── Comics/         # Comic cards, lists, forms
│   ├── Common/         # Shared components (buttons, modals)
│   ├── Layout/         # Header, footer, navigation
│   ├── Profile/        # User profile components
│   ├── Statistics/     # Charts and stats displays
│   └── Wishlist/       # Wishlist components
├── context/            # React Context providers
│   └── AuthContext.jsx # Authentication state
├── hooks/              # Custom React hooks
│   ├── useComics.js    # Comic collection logic
│   └── useWishlist.js  # Wishlist logic
├── pages/              # Page-level components
├── services/           # API service layer
│   ├── api.js          # Axios instance with interceptors
│   ├── authService.js  # Auth API calls
│   ├── comicService.js # Comic CRUD operations
│   ├── comicVineService.js # ComicVine search
│   └── wishlistService.js  # Wishlist operations
├── styles/             # CSS stylesheets
└── utils/              # Helper functions
```

## Key Features

- **Collection Management** - Add, edit, delete, and browse comics
- **ComicVine Search** - Search and import from ComicVine database
- **Wishlist** - Track comics you want to acquire
- **Statistics** - Visual analytics of your collection
- **Authentication** - JWT-based login with role support
- **Responsive Design** - Mobile-friendly interface

## Available Scripts

| Command | Description |
|---------|-------------|
| `npm start` | Run development server |
| `npm build` | Create production build |
| `npm test` | Run test suite |

## Environment Variables

Create a `.env` file for environment-specific configuration:

```env
REACT_APP_API_URL=http://localhost:5001
```

## Tech Stack

- **React 19** - UI framework
- **React Router v6** - Client-side routing
- **Axios** - HTTP client
- **Context API** - State management
- **CSS Modules** - Component styling
