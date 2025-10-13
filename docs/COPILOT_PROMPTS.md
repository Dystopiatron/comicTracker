# GitHub Copilot Chat Prompt for React Frontend

## 🎯 Quick Start Prompt

```
Create a React frontend for a Comic Collection Tracker that connects to an ASP.NET Core API running on http://localhost:5000. 

Requirements:
- Authentication (login/register) with JWT tokens
- Comic CRUD operations (add, edit, delete, view collection)
- Search and filter comics by publisher/condition
- User profile management
- Statistics dashboard
- Minimal CSS styling (no external libraries)
- React Router for navigation
- API integration with proper error handling

The backend API is already complete and running. Demo credentials: demouser/Demo123!

Key API endpoints:
- POST /api/auth/login - Login
- GET /api/comics - Get collection (supports ?search=term&publisher=Marvel&condition=6&page=1)
- POST /api/comics - Add comic
- PUT /api/comics/{id} - Update comic
- DELETE /api/comics/{id} - Delete comic
- GET /api/statistics/overview - Get stats

Comic object structure:
{
  "seriesName": "string",
  "issueNumber": "string", 
  "publisher": "string",
  "condition": 1-7 (Poor to Mint),
  "purchasePrice": number,
  "coverImageUrl": "string",
  "notes": "string"
}

Create a functional, clean UI that tests all these endpoints. Focus on functionality over aesthetics.
```

## 📋 Follow-up Prompts

### If Copilot needs more specific guidance:

**For Project Setup:**
```
Set up a new React project with create-react-app called 'comic-tracker-frontend'. Install react-router-dom. Create this folder structure:

src/
├── components/
│   ├── Auth/
│   ├── Comics/
│   ├── Layout/
│   └── Common/
├── pages/
├── services/
├── context/
└── styles/

Create a basic App.js with React Router setup for these routes: /, /login, /register, /collection, /add-comic, /profile, /stats
```

**For Authentication:**
```
Create an AuthContext that manages JWT tokens in localStorage. Include login, logout, and user state. Create a useAuth hook. Build login and register forms that call the API at http://localhost:5000/api/auth/login and /api/auth/register. Display demo credentials (demouser/Demo123!) on login form for testing.
```

**For Comic Management:**
```
Create components for:
1. ComicList - displays comics in a grid with search/filter controls
2. ComicCard - shows individual comic with edit/delete buttons  
3. ComicForm - form for adding/editing comics with all fields
4. Search and filter controls for publisher and condition

Use fetch or axios to call the API endpoints. Handle pagination with page/pageSize query params.
```

**For Statistics:**
```
Create a statistics dashboard that calls GET /api/statistics/overview and displays:
- Total comics count
- Total collection value
- Publisher breakdown
- Condition breakdown
- Most valuable comic

Use simple HTML/CSS charts or just display as lists/cards.
```

**For Styling:**
```
Create minimal CSS with these principles:
- Clean, modern look with good spacing
- Responsive grid layouts
- Simple color scheme (blue primary, gray secondary)
- Card-based design for comics
- Form styling with proper spacing
- Button styles for actions

No external CSS frameworks - just clean, custom CSS.
```

## 🔧 Technical Specifications

### API Response Format
All API responses follow this structure:
```json
{
  "success": true,
  "message": "Success message",
  "data": { /* actual data */ },
  "errors": []
}
```

### Authentication Headers
Include JWT token in requests:
```javascript
headers: {
  'Authorization': `Bearer ${token}`,
  'Content-Type': 'application/json'
}
```

### Comic Condition Enum Values
```javascript
const conditions = [
  { value: 1, label: 'Poor' },
  { value: 2, label: 'Fair' },
  { value: 3, label: 'Good' },
  { value: 4, label: 'Fine' },
  { value: 5, label: 'Very Fine' },
  { value: 6, label: 'Near Mint' },
  { value: 7, label: 'Mint' }
];
```

### Publisher Options
```javascript
const publishers = ['Marvel', 'DC', 'Image', 'DarkHorse', 'IDW', 'Valiant', 'Other'];
```

## 🎨 UI Pages Needed

1. **Home** - Dashboard with quick stats and recent comics
2. **Login** - Simple login form with demo credentials displayed
3. **Register** - Registration form
4. **Collection** - Main comic list with search/filter/pagination
5. **Add Comic** - Form to add new comic
6. **Edit Comic** - Form to edit existing comic (same as add)
7. **Profile** - User profile display and edit
8. **Statistics** - Collection analytics dashboard

## ✅ Success Criteria

The frontend is complete when you can:
- Login with demo user (demouser/Demo123!)
- View the 5 pre-loaded demo comics
- Add a new comic and see it appear
- Edit an existing comic
- Delete a comic
- Search comics by name
- Filter by publisher and condition
- View statistics page with collection data
- Navigate between all pages
- Logout and login again

Focus on functionality first, styling second. Make it work, then make it pretty!