import React from 'react';

const LoadingSpinner = ({ size = 'md', className = '' }) => {
  return (
    <div className={`loading-spinner ${className}`} style={{
      width: size === 'sm' ? '1rem' : size === 'lg' ? '2rem' : '1.5rem',
      height: size === 'sm' ? '1rem' : size === 'lg' ? '2rem' : '1.5rem'
    }}>
    </div>
  );
};

export default LoadingSpinner;