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
    <header style={{
      backgroundColor: 'var(--white)',
      borderBottom: '1px solid var(--border-color)',
      padding: '1rem 0'
    }}>
      <div className="container">
        <div className="d-flex justify-content-between align-items-center">
          <Link to="/" style={{ textDecoration: 'none', color: 'inherit' }}>
            <h1 style={{ margin: 0, fontSize: '1.5rem', color: 'var(--primary-color)' }}>
              📚 Comic Tracker
            </h1>
          </Link>
          
          <div className="d-flex align-items-center gap-3">
            {isAuthenticated ? (
              <>
                <span className="text-muted">Welcome, {user?.firstName || user?.username}!</span>
                <nav className="d-flex gap-3">
                  <Link to="/" className="btn btn-outline btn-sm">Home</Link>
                  <Link to="/collection" className="btn btn-outline btn-sm">Collection</Link>
                  <Link to="/stats" className="btn btn-outline btn-sm">Statistics</Link>
                  <Link to="/profile" className="btn btn-outline btn-sm">Profile</Link>
                </nav>
                <button onClick={handleLogout} className="btn btn-secondary btn-sm">
                  Logout
                </button>
              </>
            ) : (
              <div className="d-flex gap-2">
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