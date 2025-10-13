import React from 'react';
import StatsDashboard from '../components/Statistics/StatsDashboard';

const StatsPage = () => {
  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>Collection Statistics</h1>
          <p className="text-muted">Insights and analytics for your comic collection</p>
        </div>

        <StatsDashboard />
      </div>
    </div>
  );
};

export default StatsPage;