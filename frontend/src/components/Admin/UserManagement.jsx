import React, { useState, useEffect } from 'react';
import adminService from '../../services/adminService';
import UserEditModal from './UserEditModal';
import UserDeleteModal from './UserDeleteModal';
import UserDetailsModal from './UserDetailsModal';

const UserManagement = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedUser, setSelectedUser] = useState(null);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showDetailsModal, setShowDetailsModal] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      setLoading(true);
      setError(null);
      
      const response = await adminService.getAllUsers();
      if (response.success) {
        // Handle nested response structure: response.data.data contains the users array
        const usersData = response.data.data || response.data;
        if (Array.isArray(usersData)) {
          setUsers(usersData);
        } else {
          console.error('Expected users array but got:', usersData);
          setError('Invalid response format from server');
        }
      } else {
        setError(response.message || 'Failed to fetch users');
      }
    } catch (err) {
      console.error('Error fetching users:', err);
      setError('Failed to load users. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  const handleEditUser = (user) => {
    setSelectedUser(user);
    setShowEditModal(true);
  };

  const handleDeleteUser = (user) => {
    setSelectedUser(user);
    setShowDeleteModal(true);
  };

  const handleViewDetails = async (user) => {
    try {
      const response = await adminService.getUserWithComics(user.id);
      if (response.success) {
        // Handle nested response structure: response.data.data contains the user object
        const userData = response.data.data || response.data;
        setSelectedUser(userData);
        setShowDetailsModal(true);
      } else {
        setError(response.message || 'Failed to fetch user details');
      }
    } catch (err) {
      console.error('Error fetching user details:', err);
      setError('Failed to load user details. Please try again.');
    }
  };

  const handleUserUpdated = () => {
    setShowEditModal(false);
    setSelectedUser(null);
    fetchUsers();
  };

  const handleUserDeleted = () => {
    setShowDeleteModal(false);
    setSelectedUser(null);
    fetchUsers();
  };

  const filteredUsers = Array.isArray(users) ? users.filter(user =>
    (user.username || '').toLowerCase().includes(searchTerm.toLowerCase()) ||
    (user.email || '').toLowerCase().includes(searchTerm.toLowerCase())
  ) : [];

  if (loading) {
    return (
      <div className="text-center py-4">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
        <p className="mt-3">Loading users...</p>
      </div>
    );
  }

  return (
    <div>
      {error && (
        <div className="alert alert-danger alert-dismissible fade show" role="alert">
          {error}
          <button 
            type="button" 
            className="btn-close" 
            onClick={() => setError(null)}
            aria-label="Close"
          ></button>
        </div>
      )}

      {/* Search and Actions */}
      <div className="row mb-4">
        <div className="col-md-6">
          <div className="input-group">
            <span className="input-group-text">
              <i className="bi bi-search"></i>
            </span>
            <input
              type="text"
              className="form-control"
              placeholder="Search users by username or email..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>
        </div>
        <div className="col-md-6 text-end">
          <button 
            className="btn btn-outline-primary" 
            onClick={fetchUsers}
            disabled={loading}
          >
            <i className="bi bi-arrow-clockwise me-2"></i>
            Refresh
          </button>
        </div>
      </div>

      {/* Users Table */}
      <div className="card">
        <div className="card-header">
          <h5 className="card-title mb-0">
            <i className="bi bi-people me-2"></i>
            Users ({filteredUsers.length})
          </h5>
        </div>
        <div className="card-body p-0">
          {filteredUsers.length === 0 ? (
            <div className="text-center py-4">
              <i className="bi bi-person-x fs-1 text-muted"></i>
              <h5 className="mt-3 text-muted">No users found</h5>
              <p className="text-muted">
                {searchTerm ? 'No users match your search criteria.' : 'No users available.'}
              </p>
            </div>
          ) : (
            <div className="table-responsive">
              <table className="table table-hover mb-0">
                <thead className="table-light">
                  <tr>
                    <th>User</th>
                    <th>Email</th>
                    <th>Comics</th>
                    <th>Status</th>
                    <th>Joined</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredUsers.map((user) => (
                    <tr key={user.id}>
                      <td>
                        <div className="d-flex align-items-center">
                          <div className="me-2">👤</div>
                          <div>
                            <div className="fw-bold">{user.username || user.email || 'Unknown User'}</div>
                            {(user.isAdmin || (user.role && user.role !== 'User')) && (
                              <span className={`badge mt-1 ${
                                user.role === 'SuperAdmin' ? 'bg-danger' :
                                user.role === 'Admin' ? 'bg-warning text-dark' :
                                user.role === 'Moderator' ? 'bg-info' :
                                'bg-warning text-dark'
                              }`}>
                                {user.roleDisplayName || user.role || 'Admin'}
                              </span>
                            )}
                          </div>
                        </div>
                      </td>
                      <td>
                        <small className="text-muted">{user.email}</small>
                      </td>
                      <td>
                        <span className="badge bg-info">{user.comicCount || 0}</span>
                      </td>
                      <td>
                        <span className="badge bg-success">Active</span>
                      </td>
                      <td>
                        <small className="text-muted">
                          {new Date(user.dateCreated).toLocaleDateString('en-US', {
                            year: 'numeric',
                            month: 'short',
                            day: 'numeric'
                          })}
                        </small>
                      </td>
                      <td>
                        <div className="btn-group btn-group-sm" role="group">
                          <button
                            className="btn btn-outline-info btn-sm"
                            onClick={() => handleViewDetails(user)}
                            title="View Details"
                          >
                            👁️ View
                          </button>
                          <button
                            className="btn btn-outline-primary btn-sm"
                            onClick={() => handleEditUser(user)}
                            title="Edit User"
                          >
                            ✏️ Edit
                          </button>
                          <button
                            className="btn btn-outline-danger btn-sm"
                            onClick={() => handleDeleteUser(user)}
                            title="Delete User"
                          >
                            🗑️ Delete
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>

      {/* Modals */}
      {showEditModal && selectedUser && (
        <UserEditModal
          user={selectedUser}
          show={showEditModal}
          onHide={() => {
            setShowEditModal(false);
            setSelectedUser(null);
          }}
          onUserUpdated={handleUserUpdated}
        />
      )}

      {showDeleteModal && selectedUser && (
        <UserDeleteModal
          user={selectedUser}
          show={showDeleteModal}
          onHide={() => {
            setShowDeleteModal(false);
            setSelectedUser(null);
          }}
          onUserDeleted={handleUserDeleted}
        />
      )}

      {showDetailsModal && selectedUser && (
        <UserDetailsModal
          user={selectedUser}
          show={showDetailsModal}
          onHide={() => {
            setShowDetailsModal(false);
            setSelectedUser(null);
          }}
        />
      )}
    </div>
  );
};

export default UserManagement;