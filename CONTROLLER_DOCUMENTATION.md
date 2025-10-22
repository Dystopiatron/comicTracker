# Controller Architecture Documentation

## Overview
This application uses 7 controllers, each with a specific responsibility following the Single Responsibility Principle and RESTful API design.

## Controller Breakdown

### 1. AuthController.cs (Essential - Authentication)
**Purpose:** Handles user authentication and authorization
**Endpoints:**
- POST /api/auth/login - User login
- POST /api/auth/register - New user registration
- POST /api/auth/refresh - Refresh JWT tokens
- POST /api/auth/revoke - Logout/revoke tokens

**Why it exists:** Every secure application needs authentication

### 2. ComicsController.cs (Essential - Core Feature)
**Purpose:** Main application feature - CRUD for comic collection
**Endpoints:**
- GET /api/comics - List user's comics (paginated, filtered, sorted)
- GET /api/comics/{id} - Get specific comic
- POST /api/comics - Add comic to collection
- PUT /api/comics/{id} - Update comic details
- DELETE /api/comics/{id} - Remove comic from collection

**Why it exists:** This is the primary business function of the application

### 3. UsersController.cs (Essential - User Management)
**Purpose:** User profile self-service
**Endpoints:**
- GET /api/users/profile - View own profile
- PUT /api/users/profile - Update own information

**Why it exists:** Users need to manage their account information

### 4. AdminController.cs (Admin Features)
**Purpose:** System administration and user management
**Endpoints:**
- GET /api/admin/users - List all users (admin only)
- GET /api/admin/users/{id} - View user details
- PUT /api/admin/users/{id} - Update any user
- DELETE /api/admin/users/{id} - Delete users
- GET /api/admin/statistics - System-wide stats

**Why it exists:** Multi-user applications require administrative oversight
**Authorization:** All endpoints require Admin role

### 5. RoleController.cs (RBAC System)
**Purpose:** Role-based access control management
**Endpoints:**
- GET /api/role - List roles and permissions
- POST /api/role/promote/{userId} - Promote/demote users
- POST /api/role/revoke-tokens/{userId} - Revoke user tokens

**Why it exists:** Implements hierarchical permission system (User → Moderator → Admin → SuperAdmin)
**Security:** Prevents privilege escalation, logs all role changes

### 6. StatisticsController.cs (Analytics)
**Purpose:** Collection analytics and insights
**Endpoints:**
- GET /api/statistics/overview - User's collection statistics
- GET /api/statistics/publishers - Publisher breakdown
- GET /api/statistics/conditions - Condition analysis

**Why it exists:** Provides valuable insights about comic collection

### 7. WishlistController.cs (Feature Enhancement)
**Purpose:** Wishlist/want list management
**Endpoints:**
- GET /api/wishlist - User's wishlist items
- POST /api/wishlist - Add to wishlist
- PUT /api/wishlist/{id} - Update wishlist item
- DELETE /api/wishlist/{id} - Remove from wishlist

**Why it exists:** Post-MVP feature requested to track desired comics

## Design Principles

**Separation of Concerns:** Each controller handles one domain
**RESTful Design:** Standard HTTP verbs and resource-based URLs
**Security:** Authorization attributes on all controllers
**Testability:** Each controller can be tested independently
**Maintainability:** Focused controllers are easier to understand and modify

## Conclusion
This controller architecture follows ASP.NET Core best practices and industry standards for building maintainable, secure, scalable web APIs.