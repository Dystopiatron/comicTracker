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
      <div className="text-center" style={{ padding: '3rem 0' }}>
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
      <div className="row mb-4" style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '1rem' }}>
        <div className="card text-center">
          <h3 className="text-primary" style={{ fontSize: '2.5rem', margin: '0 0 0.5rem 0' }}>
            {stats.totalComics}
          </h3>
          <p className="text-muted mb-0">Total Comics</p>
        </div>
        <div className="card text-center">
          <h3 className="text-success" style={{ fontSize: '2.5rem', margin: '0 0 0.5rem 0' }}>
            {stats.seriesTracked}
          </h3>
          <p className="text-muted mb-0">Unique Series</p>
        </div>
        <div className="card text-center">
          <h3 className="text-warning" style={{ fontSize: '2.5rem', margin: '0 0 0.5rem 0' }}>
            {stats.publishersCount}
          </h3>
          <p className="text-muted mb-0">Publishers</p>
        </div>
        <div className="card text-center">
          <h3 className="text-danger" style={{ fontSize: '2.5rem', margin: '0 0 0.5rem 0' }}>
            {formatCurrency(stats.totalValue)}
          </h3>
          <p className="text-muted mb-0">Total Collection Value</p>
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(400px, 1fr))', gap: '1rem' }}>
        {/* Publisher Breakdown */}
        <div className="card">
          <h3>Publisher Breakdown</h3>
          {stats.publisherBreakdown && Object.keys(stats.publisherBreakdown).length > 0 ? (
            <div className="publisher-stats">
              {Object.entries(stats.publisherBreakdown).map(([publisher, count], index) => (
                <div key={publisher} className="d-flex justify-content-between align-items-center mb-2 p-2" style={{
                  backgroundColor: index % 2 === 0 ? 'var(--gray-50)' : 'transparent',
                  borderRadius: '4px'
                }}>
                  <div>
                    <strong>{publisher}</strong>
                    <div className="text-muted text-sm">
                      {count} comic{count !== 1 ? 's' : ''}
                    </div>
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
                <div key={condition} className="d-flex justify-content-between align-items-center mb-2 p-2" style={{
                  backgroundColor: index % 2 === 0 ? 'var(--gray-50)' : 'transparent',
                  borderRadius: '4px'
                }}>
                  <div>
                    <strong>{condition}</strong>
                    <div className="text-muted text-sm">
                      {count} comic{count !== 1 ? 's' : ''}
                    </div>
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