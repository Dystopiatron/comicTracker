import React, { useState, useEffect } from 'react';
import { useAuth } from '../context/AuthContext';
import UserManagement from '../components/Admin/UserManagement';
import SystemStatistics from '../components/Admin/SystemStatistics';

const AdminDashboard = () => {
  const { user } = useAuth();
  const [activeTab, setActiveTab] = useState('stats');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Check if user is admin
    if (!user?.isAdmin) {
      setError('Access denied. Admin privileges required.');
      setLoading(false);
      return;
    }
    
    setLoading(false);
  }, [user]);

  if (loading) {
    return (
      <div className="container mt-4">
        <div className="row justify-content-center">
          <div className="col-md-8 text-center">
            <div className="spinner-border" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
            <p className="mt-3">Loading admin dashboard...</p>
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="container mt-4">
        <div className="row justify-content-center">
          <div className="col-md-8">
            <div className="alert alert-danger" role="alert">
              <h4 className="alert-heading">Access Denied</h4>
              <p>{error}</p>
              <hr />
              <p className="mb-0">Please contact your system administrator if you believe this is an error.</p>
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="container-fluid mt-4">
      <div className="row">
        <div className="col-12">
          <div className="d-flex justify-content-between align-items-center mb-4">
            <h1 className="h2">
              <i className="bi bi-shield-check me-2"></i>
              Admin Dashboard
            </h1>
            <span className="badge bg-primary">Administrator</span>
          </div>

          {/* Navigation Tabs */}
          <ul className="nav nav-tabs mb-4" id="adminTabs" role="tablist">
            <li className="nav-item" role="presentation">
              <button
                className={`nav-link ${activeTab === 'stats' ? 'active' : ''}`}
                onClick={() => setActiveTab('stats')}
                type="button"
              >
                <i className="bi bi-graph-up me-2"></i>
                System Statistics
              </button>
            </li>
            <li className="nav-item" role="presentation">
              <button
                className={`nav-link ${activeTab === 'users' ? 'active' : ''}`}
                onClick={() => setActiveTab('users')}
                type="button"
              >
                <i className="bi bi-people me-2"></i>
                User Management
              </button>
            </li>
          </ul>

          {/* Tab Content */}
          <div className="tab-content" id="adminTabContent">
            {activeTab === 'stats' && (
              <div className="tab-pane fade show active">
                <SystemStatistics />
              </div>
            )}
            {activeTab === 'users' && (
              <div className="tab-pane fade show active">
                <UserManagement />
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;