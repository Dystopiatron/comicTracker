import React from 'react';
import { getConditionName, formatCurrency, formatDate } from '../../utils/helpers';
import { WISHLIST_PRIORITIES } from '../../types';
import Button from '../Common/Button';

const WishlistItemCard = ({ item, onEdit, onDelete }) => {
  const handleDelete = () => {
    if (window.confirm(`Are you sure you want to delete "${item.seriesName} ${item.issueNumber?.startsWith('#') ? item.issueNumber : `#${item.issueNumber || ''}`}" from your wishlist?`)) {
      onDelete(item.id);
    }
  };

  const getPriorityInfo = (priority) => {
    return WISHLIST_PRIORITIES.find(p => p.value === priority) || WISHLIST_PRIORITIES[3];
  };

  const priorityInfo = getPriorityInfo(item.priority);

  return (
    <div className="comic-card">
      <div className="comic-card-content">
        <div className="comic-content">
          <div className="comic-card-header">
            <div>
              <div className="d-flex align-items-center gap-2 mb-2">
                <h3 className="comic-card-title">
                  {item.seriesName} {item.issueNumber?.startsWith('#') ? item.issueNumber : `#${item.issueNumber || ''}`}
                </h3>
                <span 
                  className="wishlist-priority-badge" 
                  style={{ backgroundColor: priorityInfo.color }}
                >
                  {priorityInfo.label}
                </span>
              </div>
              <p className="comic-card-subtitle">
                {item.publisher}
              </p>
            </div>
            <div className="comic-card-actions">
              <Button
                variant="outline"
                size="sm"
                onClick={() => onEdit(item)}
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
              <strong>Desired Condition:</strong> {getConditionName(item.desiredCondition)}
            </div>
            {item.targetPrice && (
              <div className="comic-card-detail">
                <strong>Target Price:</strong> {formatCurrency(item.targetPrice)}
              </div>
            )}
            <div className="comic-card-detail">
              <strong>Added:</strong> {formatDate(item.dateAdded)}
            </div>
          </div>
          
          {item.notes && (
            <div className="comic-notes-container">
              <strong>Notes:</strong> 
              <p className="comic-notes">
                {item.notes}
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default WishlistItemCard;
