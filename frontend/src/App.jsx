import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import { apiClient } from './services/api';
import Header from './components/Layout/Header';
import Footer from './components/Layout/Footer';
import ProtectedRoute from './components/Auth/ProtectedRoute';
import AdminRoute from './components/Auth/AdminRoute';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import CollectionPage from './pages/CollectionPage';
import AddComicPage from './pages/AddComicPage';
import EditComicPage from './pages/EditComicPage';
import ProfilePage from './pages/ProfilePage';
import StatsPage from './pages/StatsPage';
import WishlistPage from './pages/WishlistPage';
import AdminDashboard from './pages/AdminDashboard';
import './styles/index.css';

// Component to handle protected/public route logic
const AppRoutes = () => {
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  // Set up 401 handler when component mounts
  useEffect(() => {
    apiClient.setUnauthorizedCallback(() => {
      console.log('Session expired - logging out user');
      logout();
      navigate('/login', { replace: true });
    });
  }, [logout, navigate]);

  return (
    <Routes>
      {/* Public routes */}
      <Route 
        path="/login" 
        element={isAuthenticated ? <Navigate to="/" replace /> : <LoginPage />} 
      />
      <Route 
        path="/register" 
        element={isAuthenticated ? <Navigate to="/" replace /> : <RegisterPage />} 
      />
      
      {/* Protected routes */}
      <Route path="/" element={
        <ProtectedRoute>
          <HomePage />
        </ProtectedRoute>
      } />
      <Route path="/collection" element={
        <ProtectedRoute>
          <CollectionPage />
        </ProtectedRoute>
      } />
      <Route path="/wishlist" element={
        <ProtectedRoute>
          <WishlistPage />
        </ProtectedRoute>
      } />
      <Route path="/add-comic" element={
        <ProtectedRoute>
          <AddComicPage />
        </ProtectedRoute>
      } />
      <Route path="/edit-comic/:id" element={
        <ProtectedRoute>
          <EditComicPage />
        </ProtectedRoute>
      } />
      <Route path="/profile" element={
        <ProtectedRoute>
          <ProfilePage />
        </ProtectedRoute>
      } />
      <Route path="/stats" element={
        <ProtectedRoute>
          <StatsPage />
        </ProtectedRoute>
      } />
      <Route path="/admin" element={
        <AdminRoute>
          <AdminDashboard />
        </AdminRoute>
      } />
      
      {/* Catch all route */}
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
};

function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="app-layout">
          <Header />
          <main className="app-main">
            <AppRoutes />
          </main>
          <Footer />
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;
