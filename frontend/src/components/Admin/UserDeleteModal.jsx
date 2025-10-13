import React, { useState } from 'react';
import adminService from '../../services/adminService';

const UserDeleteModal = ({ user, show, onHide, onUserDeleted }) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleDelete = async () => {
    try {
      setLoading(true);
      setError(null);

      const response = await adminService.deleteUser(user.id);
      
      if (response.success) {
        onUserDeleted();
      } else {
        setError(response.message || 'Failed to delete user');
      }
    } catch (err) {
      console.error('Error deleting user:', err);
      setError(err.response?.data?.message || 'Failed to delete user. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  if (!show) return null;

  return (
    <div className="modal show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title text-danger">
              <i className="bi bi-exclamation-triangle me-2"></i>
              Delete User
            </h5>
            <button type="button" className="btn-close" onClick={onHide}></button>
          </div>
          
          <div className="modal-body">
            {error && (
              <div className="alert alert-danger" role="alert">
                {error}
              </div>
            )}

            <div className="alert alert-warning" role="alert">
              <i className="bi bi-exclamation-triangle me-2"></i>
              <strong>Warning!</strong> This action cannot be undone.
            </div>

            <p>Are you sure you want to delete the following user?</p>
            
            <div className="card">
              <div className="card-body">
                <div className="d-flex align-items-center">
                  <i className="bi bi-person-circle me-3 fs-1 text-muted"></i>
                  <div>
                    <h6 className="card-title mb-1">{user?.username}</h6>
                    <p className="card-text text-muted mb-1">{user?.email}</p>
                    <small className="text-muted">
                      {user?.comicCount || 0} comics will also be deleted
                    </small>
                    {user?.isAdmin && (
                      <div className="mt-2">
                        <span className="badge bg-warning text-dark">Administrator</span>
                      </div>
                    )}
                  </div>
                </div>
              </div>
            </div>

            <p className="mt-3 mb-0">
              <strong>This will permanently delete:</strong>
            </p>
            <ul className="mb-0">
              <li>User account and profile information</li>
              <li>All associated comic book records ({user?.comicCount || 0} comics)</li>
              <li>User preferences and settings</li>
            </ul>
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
              type="button" 
              className="btn btn-danger"
              onClick={handleDelete}
              disabled={loading}
            >
              {loading ? (
                <>
                  <span className="spinner-border spinner-border-sm me-2" role="status"></span>
                  Deleting...
                </>
              ) : (
                <>
                  <i className="bi bi-trash me-2"></i>
                  Delete User
                </>
              )}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserDeleteModal;