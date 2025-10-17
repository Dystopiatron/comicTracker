import React from 'react';

const LoadingSpinner = ({ size = 'md', className = '' }) => {
  const sizeClass = size === 'sm' ? 'loading-spinner-sm' : size === 'lg' ? 'loading-spinner-lg' : '';
  
  return (
    <div className={`loading-spinner ${sizeClass} ${className}`}>
    </div>
  );
};

export default LoadingSpinner;