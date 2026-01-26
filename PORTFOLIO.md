# 📚 Comic Tracker

> A full-stack web application for comic book collectors to manage their collections, built as a capstone project demonstrating modern web development skills.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)
![React](https://img.shields.io/badge/React-19-61DAFB?style=flat&logo=react)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-336791?style=flat&logo=postgresql)

---

## 👋 About This Project

Hi! I'm Stephen, a recent software development bootcamp graduate. This is my capstone project - a comic book collection manager that I built to demonstrate my full-stack development skills.

**What makes this project special:**
- Built entirely from scratch over 6 weeks
- Integrates with the ComicVine API for real comic data
- Features secure JWT authentication
- Clean, responsive UI that works on mobile

---

## ✨ Features

### Core Functionality
- **User Authentication** - Secure registration and login with JWT tokens
- **Collection Management** - Full CRUD operations (Create, Read, Update, Delete)
- **ComicVine Integration** - Search real comics and import them with one click
- **Wishlist** - Track comics you want to acquire
- **Statistics Dashboard** - See insights about your collection

### Technical Highlights
- RESTful API design with proper HTTP methods and status codes
- Entity Framework Core with code-first migrations
- React Context API for state management
- Responsive CSS (no frameworks, all custom)
- Role-based access control (Admin/User)

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| **Frontend** | React 19, React Router v6, Axios |
| **Backend** | ASP.NET Core 9.0 Web API |
| **Database** | PostgreSQL with Entity Framework Core |
| **Auth** | JWT Bearer Tokens |
| **External API** | ComicVine REST API |

---

## 📸 Screenshots

*Coming soon - or check out the live demo!*

---

## 🚀 Running Locally

### Prerequisites
- .NET 9.0 SDK
- Node.js 18+
- PostgreSQL 15+

### Quick Start

```bash
# Clone the repo
git clone https://github.com/Dystopiatron/comicTracker.git
cd comicTracker

# Set up the database
psql -U postgres -c "CREATE DATABASE comictracker;"

# Configure backend
cd backend/src/ComicTracker.API
cp appsettings.Development.json.example appsettings.Development.json
# Edit the connection string

# Run migrations and start backend
dotnet ef database update
dotnet run

# In a new terminal, start frontend
cd frontend
npm install
npm start
```

### Demo Credentials
- **User:** `demouser` / `Demo123!`
- **Admin:** `comicfan` / `MyComics2024`

---

## 📁 Project Structure

```
comicTracker/
├── backend/
│   └── src/ComicTracker.API/
│       ├── Controllers/      # API endpoints
│       ├── Services/         # Business logic
│       ├── Models/           # Entity models
│       ├── DTOs/             # Data transfer objects
│       └── Migrations/       # Database migrations
├── frontend/
│   └── src/
│       ├── components/       # Reusable UI components
│       ├── pages/            # Page components
│       ├── services/         # API service layer
│       ├── context/          # React Context providers
│       └── hooks/            # Custom React hooks
└── docs/                     # Documentation
```

---

## 🎯 What I Learned

Building this project taught me:

1. **Full-Stack Integration** - How to connect a React frontend to a .NET backend
2. **API Design** - RESTful principles, proper status codes, error handling
3. **Authentication** - JWT tokens, secure password hashing, protected routes
4. **Database Design** - Relational modeling, migrations, EF Core
5. **External APIs** - Working with third-party APIs (ComicVine)
6. **State Management** - React Context API patterns
7. **CSS Architecture** - Organizing styles, CSS variables, responsive design

---

## 🔮 Future Improvements

Things I'd add with more time:

- [ ] Unit and integration tests
- [ ] Dark mode toggle
- [ ] Barcode scanning for quick comic entry
- [ ] Price tracking with historical data
- [ ] Export collection to CSV/PDF
- [ ] Social features (share collection, trade requests)

See [docs/CSS_REFACTOR_TODO.md](docs/CSS_REFACTOR_TODO.md) for CSS improvements I've identified.

---

## 📄 Documentation

- [API Endpoints](CONTROLLER_DOCUMENTATION.md)
- [Deployment Guide](DEPLOYMENT.md)
- [ComicVine Integration](docs/COMICVINE_INTEGRATION.md)
- [Styling Notes](docs/STYLING_NOTES.md)

---

## 👤 About Me

**Stephen Peterson**  
Junior Software Developer | Full-Stack Web Development

- 🔗 GitHub: [@Dystopiatron](https://github.com/Dystopiatron)
- 💼 LinkedIn: [Connect with me](https://linkedin.com/in/yourprofile)
- 📧 Email: peterson.stephen138@gmail.com

*I'm currently seeking junior developer opportunities! Feel free to reach out.*

---

## 📝 License

MIT License - feel free to use this as inspiration for your own projects!
