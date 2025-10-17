import React, { useState, useEffect } from 'react';
import adminService from '../../services/adminService';

const UserEditModal = ({ user, show, onHide, onUserUpdated }) => {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    firstName: '',
    lastName: '',
    isAdmin: false
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (user) {
      setFormData({
        username: user.username || '',
        email: user.email || '',
        firstName: user.firstName || '',
        lastName: user.lastName || '',
        isAdmin: user.isAdmin || false
      });
    }
  }, [user]);

  useEffect(() => {
    const handleEscape = (event) => {
      if (event.key === 'Escape') {
        onHide();
      }
    };

    if (show) {
      document.addEventListener('keydown', handleEscape);
      document.body.style.overflow = 'hidden';
    }

    return () => {
      document.removeEventListener('keydown', handleEscape);
      document.body.style.overflow = 'unset';
    };
  }, [show, onHide]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      setLoading(true);
      setError(null);

      const response = await adminService.updateUser(user.id, formData);
      
      if (response.success) {
        onUserUpdated();
      } else {
        setError(response.message || 'Failed to update user');
      }
    } catch (err) {
      console.error('Error updating user:', err);
      setError(err.response?.data?.message || 'Failed to update user. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  if (!show) return null;

  return (
    <div className="modal-backdrop" tabIndex="-1" onClick={onHide}>
      <div className="modal-dialog" onClick={(e) => e.stopPropagation()}>
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">
              <i className="bi bi-pencil me-2"></i>
              Edit User: {user?.username}
            </h5>
            <button type="button" className="btn-close" onClick={onHide}></button>
          </div>
          
          <form onSubmit={handleSubmit}>
            <div className="modal-body">
              {error && (
                <div className="alert alert-danger" role="alert">
                  {error}
                </div>
              )}

              <div className="user-edit-form">
                <div className="user-edit-row">
                  <label htmlFor="username" className="user-edit-label">Username</label>
                  <input
                    type="text"
                    className="form-control user-edit-input"
                    id="username"
                    name="username"
                    value={formData.username}
                    onChange={handleChange}
                    required
                  />
                </div>
                
                <div className="user-edit-row">
                  <label htmlFor="email" className="user-edit-label">Email</label>
                  <input
                    type="email"
                    className="form-control user-edit-input"
                    id="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                  />
                </div>

                <div className="user-edit-row">
                  <label htmlFor="firstName" className="user-edit-label">First Name</label>
                  <input
                    type="text"
                    className="form-control user-edit-input"
                    id="firstName"
                    name="firstName"
                    value={formData.firstName}
                    onChange={handleChange}
                  />
                </div>
                
                <div className="user-edit-row">
                  <label htmlFor="lastName" className="user-edit-label">Last Name</label>
                  <input
                    type="text"
                    className="form-control user-edit-input"
                    id="lastName"
                    name="lastName"
                    value={formData.lastName}
                    onChange={handleChange}
                  />
                </div>

                <div className="user-edit-row">
                  <span className="user-edit-label">Administrator privileges</span>
                  <div className="user-edit-checkbox-container">
                    <div className="form-check">
                      <input
                        className="form-check-input"
                        type="checkbox"
                        id="isAdmin"
                        name="isAdmin"
                        checked={formData.isAdmin}
                        onChange={handleChange}
                      />
                      <label className="form-check-label" htmlFor="isAdmin">
                        <i className="bi bi-shield-check me-2"></i>
                        Enable admin access
                      </label>
                    </div>
                    <small className="text-muted">
                      Administrators can manage users and access all system features.
                    </small>
                  </div>
                </div>
              </div>

              <div className="alert alert-info" role="alert">
                <i className="bi bi-info-circle me-2"></i>
                <strong>User Information:</strong>
                <ul className="mb-0 mt-2">
                  <li>User ID: {user?.id}</li>
                  <li>Comics Count: {user?.comicCount || 0}</li>
                  <li>Account Created: {user?.dateCreated ? new Date(user.dateCreated).toLocaleDateString() : 'N/A'}</li>
                </ul>
              </div>
            </div>
            
            <div className="modal-footer">
              <button 
                type="button" 
                className="btn btn-secondary" 
                onClick={onHide}
                disabled={loading}
              >
                Cancel
              </button>
              <button 
                type="submit" 
                className="btn btn-primary"
                disabled={loading}
              >
                {loading ? (
                  <>
                    <span className="spinner-border spinner-border-sm me-2" role="status"></span>
                    Updating...
                  </>
                ) : (
                  <>
                    <i className="bi bi-check-lg me-2"></i>
                    Update User
                  </>
                )}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default UserEditModal;