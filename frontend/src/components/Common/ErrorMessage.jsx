import React from 'react';

const ErrorMessage = ({ message, type = 'error', onDismiss }) => {
  const alertClass = `alert alert-${type}`;

  return (
    <div className={alertClass}>
      <div className="d-flex justify-content-between align-items-center">
        <span>{message}</span>
        {onDismiss && (
          <button
            onClick={onDismiss}
            className="btn btn-sm"
            style={{ 
              background: 'none', 
              border: 'none', 
              color: 'inherit',
              fontSize: '1.2rem',
              lineHeight: 1,
              padding: '0 0.25rem'
            }}
          >
            ×
          </button>
        )}
      </div>
    </div>
  );
};

export default ErrorMessage;