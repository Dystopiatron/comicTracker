import React, { useState } from 'react';
import UserManagement from '../components/Admin/UserManagement';
import SystemStatistics from '../components/Admin/SystemStatistics';

const AdminDashboard = () => {
  const [activeTab, setActiveTab] = useState('stats');

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