import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { comicService } from '../services/comicService';
import { formatCurrency } from '../utils/helpers';
import LoadingSpinner from '../components/Common/LoadingSpinner';
import ErrorMessage from '../components/Common/ErrorMessage';

const HomePage = () => {
  const { user } = useAuth();
  const [recentComics, setRecentComics] = useState([]);
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchDashboardData = async () => {
      try {
        setLoading(true);
        setError('');

        // Fetch recent comics (first 5)
        const comicsResponse = await comicService.getComics(1, 5);
        if (comicsResponse.success && comicsResponse.data) {
          // Handle double-wrapped response
          const actualData = comicsResponse.data.data || comicsResponse.data;
          setRecentComics(actualData.items || []);
        }

        // Fetch statistics
        const statsResponse = await comicService.getStatistics();
        if (statsResponse.success && statsResponse.data) {
          // Handle double-wrapped response
          const actualStats = statsResponse.data.data || statsResponse.data;
          setStats(actualStats);
        }
      } catch (err) {
        setError('Failed to load dashboard data');
      } finally {
        setLoading(false);
      }
    };

    fetchDashboardData();
  }, []);

  if (loading) {
    return (
      <div className="container">
        <div className="text-center loading-section">
          <LoadingSpinner size="lg" />
          <p className="mt-2 text-muted">Loading dashboard...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="container">
      <div className="content-wrapper">
        {/* Welcome Header */}
        <div className="text-center mb-4">
          <h1>Welcome to Comic Tracker</h1>
          <p className="text-muted">
            {user ? `Hello, ${user.data?.firstName || user.firstName || user.firstname || user.data?.username || user.username || 'User'}! Here's your collection overview.` : 'Manage and track your comic book collection.'}
          </p>
        </div>

        {error && <ErrorMessage message={error} onDismiss={() => setError('')} />}

        {/* Quick Stats */}
        {stats && (
          <div className="stats-grid mb-4">
            <div className="card text-center">
              <h3 className="text-primary stat-number">
                {stats.totalComics}
              </h3>
              <p className="text-muted mb-0">Total Comics</p>
            </div>
            <div className="card text-center">
              <h3 className="text-success stat-number">
                {stats.seriesTracked}
              </h3>
              <p className="text-muted mb-0">Series</p>
            </div>
            <div className="card text-center">
              <h3 className="text-warning stat-number">
                {stats.publishersCount}
              </h3>
              <p className="text-muted mb-0">Publishers</p>
            </div>
            <div className="card text-center">
              <h3 className="text-danger stat-number">
                {formatCurrency(stats.totalValue)}
              </h3>
              <p className="text-muted mb-0">Total Value</p>
            </div>
          </div>
        )}

        {/* Quick Actions */}
        <div className="card mb-4">
          <h3>Quick Actions</h3>
          <div className="d-flex gap-3 flex-wrap">
            <Link to="/add-comic" className="btn btn-primary">
              Add New Comic
            </Link>
            <Link to="/collection" className="btn btn-outline">
              View Collection
            </Link>
            <Link to="/stats" className="btn btn-outline">
              View Statistics
            </Link>
          </div>
        </div>

        {/* Recent Comics */}
        {recentComics.length > 0 && (
          <div className="card">
            <div className="d-flex justify-content-between align-items-center mb-3">
              <h3 className="section-header">Recently Added Comics</h3>
              <Link to="/collection" className="btn btn-outline btn-sm">
                View All
              </Link>
            </div>
            
            <div className="recent-comics">
              {recentComics.map(comic => (
                <div key={comic.id} className="d-flex align-items-center gap-3 p-3 recent-comic-item">
                  {comic.coverImageUrl && (
                    <img
                      src={comic.coverImageUrl}
                      alt={`${comic.seriesName} ${comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber}`}`}
                      className="comic-cover"
                      onError={(e) => {
                        e.currentTarget.style.display = 'none';
                      }}
                    />
                  )}
                  <div className="comic-details">
                    <h4 className="comic-title">
                      {comic.seriesName} {comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber || ''}`}
                    </h4>
                    <p className="text-muted comic-meta">
                      {comic.publisher} • {formatCurrency(comic.purchasePrice)}
                    </p>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}

        {/* Empty State */}
        {stats?.totalComics === 0 && (
          <div className="card text-center empty-state">
            <h3>Start Your Collection</h3>
            <p className="text-muted mb-3">
              You haven't added any comics yet. Get started by adding your first comic to your collection!
            </p>
            <Link to="/add-comic" className="btn btn-primary btn-lg">
              Add Your First Comic
            </Link>
          </div>
        )}
      </div>
    </div>
  );
};

export default HomePage;