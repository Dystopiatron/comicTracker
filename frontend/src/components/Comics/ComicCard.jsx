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
    <div className="comic-card">
      <div className="comic-card-content">
        {comic.coverImageUrl && (
          <div className="comic-image-container">
            <img
              src={comic.coverImageUrl}
              alt={`${comic.seriesName} ${comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber}`}`}
              className="comic-cover-image"
              onError={(e) => {
                e.currentTarget.style.display = 'none';
              }}
            />
          </div>
        )}
        
        <div className="comic-content">
          <div className="comic-card-header">
            <div>
              <h3 className="comic-card-title">
                {comic.seriesName} {comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber || ''}`}
              </h3>
              <p className="comic-card-subtitle">
                {comic.publisher}
              </p>
            </div>
            <div className="comic-card-actions">
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
          
          <div className="comic-card-details">
            <div className="comic-card-detail">
              <strong>Condition:</strong> {getConditionName(comic.condition)}
            </div>
            <div className="comic-card-detail">
              <strong>Price:</strong> {formatCurrency(comic.purchasePrice)}
            </div>
            <div className="comic-card-detail">
              <strong>Added:</strong> {formatDate(comic.dateAdded)}
            </div>
          </div>
          
          {comic.notes && (
            <div className="comic-notes-container">
              <strong>Notes:</strong> 
              <p className="comic-notes">
                {comic.notes}
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default ComicCard;