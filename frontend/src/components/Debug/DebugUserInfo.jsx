import React from 'react';
import { useAuth } from '../../context/AuthContext';

const DebugUserInfo = () => {
  const { user, token, isAuthenticated } = useAuth();
  
  // Get raw data from localStorage for comparison
  const rawUser = localStorage.getItem('user');
  const rawToken = localStorage.getItem('token');
  
  return (
    <div style={{ 
      position: 'fixed', 
      bottom: '10px', 
      left: '10px', 
      background: '#f8f9fa', 
      border: '1px solid #ccc', 
      padding: '10px', 
      borderRadius: '5px',
      fontSize: '12px',
      maxWidth: '250px',
      zIndex: 1000,
      maxHeight: '300px',
      overflow: 'auto'
    }}>
      <h6>Debug Info:</h6>
      <p><strong>Is Authenticated:</strong> {isAuthenticated ? 'Yes' : 'No'}</p>
      <p><strong>User exists:</strong> {user ? 'Yes' : 'No'}</p>
      <p><strong>User.isAdmin:</strong> {user?.isAdmin ? 'Yes' : 'No'}</p>
      <p><strong>Username:</strong> {user?.username || 'N/A'}</p>
      <p><strong>Email:</strong> {user?.email || 'N/A'}</p>
      <p><strong>Token exists:</strong> {token ? 'Yes' : 'No'}</p>
      
      <details>
        <summary>Raw User Object:</summary>
        <pre style={{ fontSize: '10px', maxHeight: '100px', overflow: 'auto' }}>
          {JSON.stringify(user, null, 2)}
        </pre>
      </details>
      
      <details>
        <summary>Raw LocalStorage User:</summary>
        <pre style={{ fontSize: '10px', maxHeight: '100px', overflow: 'auto' }}>
          {rawUser}
        </pre>
      </details>
    </div>
  );
};

export default DebugUserInfo;