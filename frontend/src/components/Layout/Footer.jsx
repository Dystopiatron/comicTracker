import React from 'react';

const Footer = () => {
  return (
    <footer style={{
      backgroundColor: 'var(--gray-100)',
      borderTop: '1px solid var(--border-color)',
      padding: '1rem 0',
      marginTop: 'auto'
    }}>
      <div className="container">
        <div className="text-center text-muted">
          <p style={{ margin: 0, fontSize: '0.875rem' }}>
            © 2025 Comic Collection Tracker. Built with React & ASP.NET Core.
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;