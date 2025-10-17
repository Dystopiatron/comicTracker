import React, { useEffect } from 'react';

const UserDetailsModal = ({ user, show, onHide }) => {
  useEffect(() => {
    const handleEscape = (event) => {
      if (event.key === 'Escape') {
        onHide();
      }
    };

    if (show) {
      document.addEventListener('keydown', handleEscape);
      // Prevent body scrolling when modal is open
      document.body.style.overflow = 'hidden';
    }

    return () => {
      document.removeEventListener('keydown', handleEscape);
      document.body.style.overflow = 'unset';
    };
  }, [show, onHide]);

  if (!show) return null;

  return (
    <div className="modal-backdrop" tabIndex="-1" onClick={onHide}>
      <div className="modal-dialog modal-lg" onClick={(e) => e.stopPropagation()}>
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">
              <i className="bi bi-person-lines-fill me-2"></i>
              User Details: {user?.username}
            </h5>
            <button type="button" className="btn-close" onClick={onHide}></button>
          </div>
          
          <div className="modal-body">
            {/* User Information */}
            <div className="row mb-4">
              <div className="col-md-6">
                <div className="card">
                  <div className="card-header">
                    <h6 className="card-title mb-0">
                      <i className="bi bi-person me-2"></i>
                      User Information
                    </h6>
                  </div>
                  <div className="card-body">
                    <div className="user-details-list">
                      <div className="user-detail-row">
                        <span className="user-detail-label">Username:</span>
                        <span className="user-detail-value">{user?.username || 'N/A'}</span>
                      </div>
                      
                      <div className="user-detail-row">
                        <span className="user-detail-label">Email:</span>
                        <span className="user-detail-value">{user?.email || 'N/A'}</span>
                      </div>
                      
                      <div className="user-detail-row">
                        <span className="user-detail-label">First Name:</span>
                        <span className="user-detail-value">{user?.firstName || 'N/A'}</span>
                      </div>
                      
                      <div className="user-detail-row">
                        <span className="user-detail-label">Last Name:</span>
                        <span className="user-detail-value">{user?.lastName || 'N/A'}</span>
                      </div>
                      
                      <div className="user-detail-row">
                        <span className="user-detail-label">Status:</span>
                        <span className="user-detail-value">
                          <span className="badge bg-success">Active</span>
                          {user?.isAdmin && (
                            <span className="badge bg-warning text-dark ms-2">Admin</span>
                          )}
                        </span>
                      </div>
                      
                      <div className="user-detail-row">
                        <span className="user-detail-label">Joined:</span>
                        <span className="user-detail-value">
                          {user?.dateCreated ? 
                            new Date(user.dateCreated).toLocaleDateString('en-US', {
                              year: 'numeric',
                              month: 'long',
                              day: 'numeric',
                              hour: '2-digit',
                              minute: '2-digit'
                            }) : 'N/A'
                          }
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div className="col-md-6">
                <div className="card">
                  <div className="card-header">
                    <h6 className="card-title mb-0">
                      <i className="bi bi-graph-up me-2"></i>
                      Collection Statistics
                    </h6>
                  </div>
                  <div className="card-body">
                    <div className="row text-center">
                      <div className="col-6">
                        <div className="border-end">
                          <h4 className="text-primary">{user?.comics?.length || 0}</h4>
                          <small className="text-muted">Total Comics</small>
                        </div>
                      </div>
                      <div className="col-6">
                        <h4 className="text-success">
                          {user?.comics ? 
                            [...new Set(user.comics.map(comic => comic.publisher).filter(p => p))].length : 0
                          }
                        </h4>
                        <small className="text-muted">Publishers</small>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            {/* Comics Collection */}
            <div className="card">
              <div className="card-header">
                <h6 className="card-title mb-0">
                  <i className="bi bi-collection me-2"></i>
                  Comic Collection ({user?.comics?.length || 0})
                </h6>
              </div>
              <div className="card-body">
                {user?.comics && user.comics.length > 0 ? (
                  <div className="table-responsive modal-scrollable">
                    <table className="table table-striped">
                      <thead className="table-light sticky-top">
                        <tr>
                          <th>Title</th>
                          <th>Issue #</th>
                          <th>Publisher</th>
                          <th>Condition</th>
                          <th>Value</th>
                        </tr>
                      </thead>
                      <tbody>
                        {user.comics.map((comic) => (
                          <tr key={comic.id}>
                            <td>
                              <div>
                                <div className="fw-bold">{comic.seriesName}</div>
                              </div>
                            </td>
                            <td>{comic.issueNumber}</td>
                            <td>
                              <span className="badge bg-light text-dark">{comic.publisher}</span>
                            </td>
                            <td>
                              <span className={`badge ${getConditionBadgeClass(comic.condition)}`}>
                                {comic.condition}
                              </span>
                            </td>
                            <td>
                              {comic.purchasePrice ? `$${comic.purchasePrice.toFixed(2)}` : 'N/A'}
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                ) : (
                  <div className="text-center py-4">
                    <i className="bi bi-collection fs-1 text-muted"></i>
                    <h6 className="mt-3 text-muted">No Comics Found</h6>
                    <p className="text-muted mb-0">This user hasn't added any comics to their collection yet.</p>
                  </div>
                )}
              </div>
            </div>
          </div>
          
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={onHide}>
              <i className="bi bi-x-lg me-2"></i>
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

// Helper function to get condition badge class
const getConditionBadgeClass = (condition) => {
  switch (condition?.toLowerCase()) {
    case 'mint':
      return 'bg-success';
    case 'near mint':
      return 'bg-info';
    case 'very fine':
      return 'bg-primary';
    case 'fine':
      return 'bg-warning text-dark';
    case 'very good':
      return 'bg-secondary';
    case 'good':
      return 'bg-warning text-dark';
    case 'fair':
      return 'bg-danger';
    case 'poor':
      return 'bg-dark';
    default:
      return 'bg-light text-dark';
  }
};

export default UserDetailsModal;