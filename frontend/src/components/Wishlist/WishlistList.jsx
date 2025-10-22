import React from 'react';
import WishlistItemCard from './WishlistItemCard';
import LoadingSpinner from '../Common/LoadingSpinner';
import ErrorMessage from '../Common/ErrorMessage';
import Button from '../Common/Button';

const WishlistList = ({
  wishlistItems,
  loading,
  error,
  currentPage,
  totalPages,
  totalCount,
  onEdit,
  onDelete,
  onPageChange
}) => {
  if (loading) {
    return (
      <div className="loading-container">
        <LoadingSpinner size="lg" />
        <p>Loading wishlist...</p>
      </div>
    );
  }

  if (error) {
    return <ErrorMessage message={error} type="error" />;
  }

  if (!wishlistItems || wishlistItems.length === 0) {
    return (
      <div className="empty-state">
        <div className="empty-state-icon">📝</div>
        <h3>Your Wishlist is Empty</h3>
        <p>Start adding comics you want to acquire to your wishlist!</p>
      </div>
    );
  }

  const renderPagination = () => {
    if (totalPages <= 1) return null;

    return (
      <div className="pagination-container">
        <div className="pagination">
          <Button
            variant="outline"
            size="sm"
            onClick={() => onPageChange(currentPage - 1)}
            disabled={currentPage === 1}
          >
            Previous
          </Button>

          <span className="pagination-info">
            Page {currentPage} of {totalPages} ({totalCount} items)
          </span>

          <Button
            variant="outline"
            size="sm"
            onClick={() => onPageChange(currentPage + 1)}
            disabled={currentPage === totalPages}
          >
            Next
          </Button>
        </div>
      </div>
    );
  };

  return (
    <div className="wishlist-list">
      <div className="mb-3">
        <p className="text-muted">
          Showing {wishlistItems.length} of {totalCount} items
        </p>
      </div>

      <div className="comic-grid">
        {wishlistItems.map(item => (
          <WishlistItemCard
            key={item.id}
            item={item}
            onEdit={onEdit}
            onDelete={onDelete}
          />
        ))}
      </div>

      {renderPagination()}
    </div>
  );
};

export default WishlistList;
