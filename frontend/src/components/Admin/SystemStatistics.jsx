import React, { useState, useEffect } from 'react';
import adminService from '../../services/adminService';

const SystemStatistics = () => {
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchStatistics();
  }, []);

  const fetchStatistics = async () => {
    try {
      setLoading(true);
      setError(null);
      
      const response = await adminService.getSystemStatistics();
      if (response.success) {
        // Handle nested response structure: response.data.data contains the statistics
        const statsData = response.data.data || response.data;
        setStats(statsData);
      } else {
        setError(response.message || 'Failed to fetch statistics');
      }
    } catch (err) {
      console.error('Error fetching statistics:', err);
      setError('Failed to load system statistics. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="text-center py-4">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
        <p className="mt-3">Loading system statistics...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="alert alert-danger" role="alert">
        <h4 className="alert-heading">Error Loading Statistics</h4>
        <p>{error}</p>
        <button className="btn btn-outline-danger" onClick={fetchStatistics}>
          <i className="bi bi-arrow-clockwise me-2"></i>
          Retry
        </button>
      </div>
    );
  }

  if (!stats) {
    return (
      <div className="alert alert-info" role="alert">
        No statistics data available.
      </div>
    );
  }

  return (
    <div className="row">
      {/* Summary Cards */}
      <div className="col-md-3 mb-4">
        <div className="card bg-primary text-white">
          <div className="card-body">
            <div className="d-flex justify-content-between">
              <div>
                <h4 className="card-title">{stats.totalUsers}</h4>
                <p className="card-text">Total Users</p>
              </div>
              <i className="bi bi-people fs-1 opacity-50"></i>
            </div>
          </div>
        </div>
      </div>

      <div className="col-md-3 mb-4">
        <div className="card bg-success text-white">
          <div className="card-body">
            <div className="d-flex justify-content-between">
              <div>
                <h4 className="card-title">{stats.totalAdmins}</h4>
                <p className="card-text">Administrators</p>
              </div>
              <i className="bi bi-shield-check fs-1 opacity-50"></i>
            </div>
          </div>
        </div>
      </div>

      <div className="col-md-3 mb-4">
        <div className="card bg-info text-white">
          <div className="card-body">
            <div className="d-flex justify-content-between">
              <div>
                <h4 className="card-title">{stats.totalComics}</h4>
                <p className="card-text">Total Comics</p>
              </div>
              <i className="bi bi-book fs-1 opacity-50"></i>
            </div>
          </div>
        </div>
      </div>

      <div className="col-md-3 mb-4">
        <div className="card bg-warning text-dark">
          <div className="card-body">
            <div className="d-flex justify-content-between">
              <div>
                <h4 className="card-title">
                  {stats.totalUsers > 0 ? Math.round(stats.totalComics / stats.totalUsers) : 0}
                </h4>
                <p className="card-text">Avg Comics/User</p>
              </div>
              <i className="bi bi-calculator fs-1 opacity-50"></i>
            </div>
          </div>
        </div>
      </div>

      {/* Recent Users */}
      <div className="col-12">
        <div className="card">
          <div className="card-header">
            <h5 className="card-title mb-0">
              <i className="bi bi-clock-history me-2"></i>
              Recent Users
            </h5>
          </div>
          <div className="card-body">
            {stats.recentUsers && stats.recentUsers.length > 0 ? (
              <div className="table-responsive">
                <table className="table table-hover">
                  <thead>
                    <tr>
                      <th>Username</th>
                      <th>Registration Date</th>
                    </tr>
                  </thead>
                  <tbody>
                    {stats.recentUsers.map((user, index) => (
                      <tr key={index}>
                        <td>
                          <i className="bi bi-person-circle me-2"></i>
                          {user.userName}
                        </td>
                        <td>
                          <small className="text-muted">
                            {new Date(user.dateCreated).toLocaleDateString('en-US', {
                              year: 'numeric',
                              month: 'short',
                              day: 'numeric',
                              hour: '2-digit',
                              minute: '2-digit'
                            })}
                          </small>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            ) : (
              <p className="text-muted mb-0">No recent users found.</p>
            )}
          </div>
        </div>
      </div>

      {/* Refresh Button */}
      <div className="col-12 mt-3">
        <button 
          className="btn btn-outline-primary" 
          onClick={fetchStatistics}
          disabled={loading}
        >
          <i className="bi bi-arrow-clockwise me-2"></i>
          Refresh Statistics
        </button>
      </div>
    </div>
  );
};

export default SystemStatistics;