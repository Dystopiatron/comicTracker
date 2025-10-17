import React, { useState, useEffect } from 'react';
import { comicService } from '../../services/comicService';
import { formatCurrency } from '../../utils/helpers';
import LoadingSpinner from '../Common/LoadingSpinner';
import ErrorMessage from '../Common/ErrorMessage';

const StatsDashboard = () => {
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchStats = async () => {
      try {
        setLoading(true);
        setError('');

        const response = await comicService.getStatistics();
        
        if (response.success && response.data) {
          // Handle double-wrapped response
          const actualStats = response.data.data || response.data;
          setStats(actualStats);
        } else {
          setError(response.message);
        }
      } catch (err) {
        setError('Failed to load statistics');
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, []);

  if (loading) {
    return (
      <div className="text-center loading-section">
        <LoadingSpinner size="lg" />
        <p className="mt-2 text-muted">Loading statistics...</p>
      </div>
    );
  }

  if (error) {
    return <ErrorMessage message={error} onDismiss={() => setError('')} />;
  }

  if (!stats) {
    return (
      <div className="card text-center">
        <h3>No Statistics Available</h3>
        <p className="text-muted">Add some comics to your collection to see statistics!</p>
      </div>
    );
  }

  return (
    <div>
      {/* Overview Cards */}
            <div className="stats-grid mb-4">
        <div className="card text-center">
          <h3 className="text-primary stats-large-number">
            {stats.totalComics}
          </h3>
          <p className="text-muted mb-0">Total Comics</p>
        </div>
        <div className="card text-center">
          <h3 className="text-success stats-large-number">
            {stats.seriesTracked}
          </h3>
          <p className="text-muted mb-0">Series Tracked</p>
        </div>
        <div className="card text-center">
          <h3 className="text-warning stats-large-number">
            {stats.publishersCount}
          </h3>
          <p className="text-muted mb-0">Publishers</p>
        </div>
        <div className="card text-center">
          <h3 className="text-danger stats-large-number">
            {formatCurrency(stats.totalValue)}
          </h3>
          <p className="text-muted mb-0">Total Value</p>
        </div>
      </div>

      <div className="stats-charts-grid">
        {/* Publisher Breakdown */}
        <div className="card">
          <h3>Publisher Breakdown</h3>
          {stats.publisherBreakdown && Object.keys(stats.publisherBreakdown).length > 0 ? (
            <div className="publisher-stats">
              {Object.entries(stats.publisherBreakdown).map(([publisher, count], index) => (
                <div key={publisher} className={`stats-breakdown-row mb-2 p-2 ${index % 2 === 0 ? 'stat-item' : ''}`}>
                  <div className="stats-breakdown-label">
                    <strong>{publisher}</strong>
                  </div>
                  <div className="stats-breakdown-value">
                    <strong>{count} comic{count !== 1 ? 's' : ''}</strong>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-muted">No publisher data available</p>
          )}
        </div>

        {/* Condition Breakdown */}
        <div className="card">
          <h3>Condition Breakdown</h3>
          {stats.conditionBreakdown && Object.keys(stats.conditionBreakdown).length > 0 ? (
            <div className="condition-stats">
              {Object.entries(stats.conditionBreakdown)
                .sort(([conditionA], [conditionB]) => {
                  // Define condition order (best to worst)
                  const conditionOrder = {
                    'Mint': 1,
                    'NearMint': 2,
                    'VeryFine': 3,
                    'Fine': 4,
                    'VeryGood': 5,
                    'Good': 6,
                    'Fair': 7,
                    'Poor': 8
                  };
                  return (conditionOrder[conditionA] || 9) - (conditionOrder[conditionB] || 9);
                })
                .map(([condition, count], index) => (
                <div key={condition} className={`stats-breakdown-row mb-2 p-2 ${index % 2 === 0 ? 'stat-item' : ''}`}>
                  <div className="stats-breakdown-label">
                    <strong>{condition}</strong>
                  </div>
                  <div className="stats-breakdown-value">
                    <strong>{count} comic{count !== 1 ? 's' : ''}</strong>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-muted">No condition data available</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default StatsDashboard;