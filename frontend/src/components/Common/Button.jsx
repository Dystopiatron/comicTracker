import React from 'react';
import LoadingSpinner from './LoadingSpinner';

const Button = ({
  variant = 'primary',
  size = 'md',
  loading = false,
  children,
  className = '',
  disabled,
  ...props
}) => {
  const baseClasses = 'btn';
  const variantClass = `btn-${variant}`;
  const sizeClass = size !== 'md' ? `btn-${size}` : '';
  
  const classes = `${baseClasses} ${variantClass} ${sizeClass} ${className}`.trim();

  return (
    <button
      className={classes}
      disabled={disabled || loading}
      {...props}
    >
      {loading && <LoadingSpinner size="sm" />}
      {loading ? ' Loading...' : children}
    </button>
  );
};

export default Button;