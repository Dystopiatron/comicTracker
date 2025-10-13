import React from 'react';
import { getConditionName, formatCurrency, formatDate } from '../../utils/helpers';
import Button from '../Common/Button';

const ComicCard = ({ comic, onEdit, onDelete }) => {
  const handleDelete = () => {
    if (window.confirm(`Are you sure you want to delete "${comic.seriesName} ${comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber || ''}`}"?`)) {
      onDelete(comic.id);
    }
  };

  return (
    <div className="card">
      <div className="d-flex gap-3">
        {comic.coverImageUrl && (
          <div style={{ flexShrink: 0 }}>
            <img
              src={comic.coverImageUrl}
              alt={`${comic.seriesName} ${comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber}`}`}
              style={{
                width: '80px',
                height: '120px',
                objectFit: 'cover',
                borderRadius: '4px',
                backgroundColor: 'var(--gray-100)'
              }}
              onError={(e) => {
                e.currentTarget.style.display = 'none';
              }}
            />
          </div>
        )}
        
        <div style={{ flex: 1, minWidth: 0 }}>
          <div className="d-flex justify-content-between align-items-start">
            <div>
              <h3 className="card-title" style={{ marginBottom: '0.25rem' }}>
                {comic.seriesName} {comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber || ''}`}
              </h3>
              <p className="card-subtitle text-muted">
                {comic.publisher}
              </p>
            </div>
            <div className="d-flex gap-2">
              <Button
                variant="outline"
                size="sm"
                onClick={() => onEdit(comic)}
              >
                Edit
              </Button>
              <Button
                variant="danger"
                size="sm"
                onClick={handleDelete}
              >
                Delete
              </Button>
            </div>
          </div>
          
          <div className="mt-3">
            <div className="d-flex gap-4 text-sm">
              <div>
                <strong>Condition:</strong> {getConditionName(comic.condition)}
              </div>
              <div>
                <strong>Price:</strong> {formatCurrency(comic.purchasePrice)}
              </div>
              <div>
                <strong>Added:</strong> {formatDate(comic.dateAdded)}
              </div>
            </div>
            
            {comic.notes && (
              <div className="mt-2">
                <strong>Notes:</strong> 
                <p className="text-muted mt-1" style={{ margin: 0 }}>
                  {comic.notes}
                </p>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ComicCard;