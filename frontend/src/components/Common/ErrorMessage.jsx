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
            className="alert-close-btn"
          >
            ×
          </button>
        )}
      </div>
    </div>
  );
};

export default ErrorMessage;