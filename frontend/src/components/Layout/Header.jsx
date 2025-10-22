import React from 'react';
import { useAuth } from '../../context/AuthContext';
import { Link, useNavigate } from 'react-router-dom';

const Header = () => {
  const { user, logout, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    console.log('Logout button clicked');
    try {
      logout();
      navigate('/login');
    } catch (error) {
      console.error('Error during logout:', error);
    }
  };

  return (
    <header className="site-header">
      <div className="container">
        <div className="d-flex justify-content-between align-items-center">
          <Link to="/" className="site-logo">
            <h1 className="logo-title">
              📚 Comic Tracker
            </h1>
          </Link>
          
          <div className="header-actions">
            {isAuthenticated ? (
              <>
                <span className="header-welcome">Welcome, {user?.data?.firstName || user?.firstName || user?.firstname || user?.data?.username || user?.username || 'User'}!</span>
                <nav className="header-nav">
                  <Link to="/" className="btn btn-outline btn-sm">Home</Link>
                  <Link to="/collection" className="btn btn-outline btn-sm">Collection</Link>
                  <Link to="/wishlist" className="btn btn-outline btn-sm">Wishlist</Link>
                  <Link to="/stats" className="btn btn-outline btn-sm">Statistics</Link>
                  <Link to="/profile" className="btn btn-outline btn-sm">Profile</Link>
                  {user?.isAdmin && (
                    <Link to="/admin" className="btn btn-warning btn-sm">
                      <i className="bi bi-shield-check me-1"></i>
                      Admin
                    </Link>
                  )}
                </nav>
                <button onClick={handleLogout} className="btn btn-secondary btn-sm">
                  Logout
                </button>
              </>
            ) : (
              <div className="header-nav">
                <Link to="/login" className="btn btn-primary btn-sm">Login</Link>
                <Link to="/register" className="btn btn-outline btn-sm">Register</Link>
              </div>
            )}
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;