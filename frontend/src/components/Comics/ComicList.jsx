import React from 'react';
import ComicCard from './ComicCard';
import LoadingSpinner from '../Common/LoadingSpinner';
import ErrorMessage from '../Common/ErrorMessage';
import Button from '../Common/Button';

const ComicList = ({
  comics = [],
  loading,
  error,
  currentPage,
  totalPages,
  totalCount,
  onEdit,
  onDelete,
  onPageChange
}) => {
  // Ensure comics is always an array to prevent map errors
  const safeComics = Array.isArray(comics) ? comics : [];
  if (loading) {
    return (
      <div className="text-center loading-section">
        <LoadingSpinner size="lg" />
        <p className="mt-2 text-muted">Loading comics...</p>
      </div>
    );
  }

  if (error) {
    return <ErrorMessage message={error} type="error" />;
  }

  if (safeComics.length === 0) {
    return (
      <div className="card text-center empty-state">
        <h3>No Comics Found</h3>
        <p className="text-muted">Start building your collection by adding your first comic!</p>
      </div>
    );
  }

  // Generate pagination buttons
  const generatePaginationButtons = () => {
    const buttons = [];
    const maxVisibleButtons = 5;
    
    let startPage = Math.max(1, currentPage - Math.floor(maxVisibleButtons / 2));
    let endPage = Math.min(totalPages, startPage + maxVisibleButtons - 1);
    
    // Adjust startPage if we're near the end
    if (endPage - startPage + 1 < maxVisibleButtons) {
      startPage = Math.max(1, endPage - maxVisibleButtons + 1);
    }

    // Previous button
    if (currentPage > 1) {
      buttons.push(
        <Button
          key="prev"
          variant="outline"
          size="sm"
          onClick={() => onPageChange(currentPage - 1)}
        >
          Previous
        </Button>
      );
    }

    // First page if not visible
    if (startPage > 1) {
      buttons.push(
        <Button
          key={1}
          variant="outline"
          size="sm"
          onClick={() => onPageChange(1)}
        >
          1
        </Button>
      );
      if (startPage > 2) {
        buttons.push(<span key="dots1" className="px-2">...</span>);
      }
    }

    // Visible page buttons
    for (let page = startPage; page <= endPage; page++) {
      buttons.push(
        <Button
          key={page}
          variant={page === currentPage ? "primary" : "outline"}
          size="sm"
          onClick={() => onPageChange(page)}
        >
          {page}
        </Button>
      );
    }

    // Last page if not visible
    if (endPage < totalPages) {
      if (endPage < totalPages - 1) {
        buttons.push(<span key="dots2" className="px-2">...</span>);
      }
      buttons.push(
        <Button
          key={totalPages}
          variant="outline"
          size="sm"
          onClick={() => onPageChange(totalPages)}
        >
          {totalPages}
        </Button>
      );
    }

    // Next button
    if (currentPage < totalPages) {
      buttons.push(
        <Button
          key="next"
          variant="outline"
          size="sm"
          onClick={() => onPageChange(currentPage + 1)}
        >
          Next
        </Button>
      );
    }

    return buttons;
  };

  return (
    <div>
      {/* Results summary */}
      <div className="d-flex justify-content-between align-items-center mb-3">
        <p className="text-muted mb-0">
          Showing {safeComics.length} of {totalCount} comics
          {totalPages > 1 && ` (Page ${currentPage} of ${totalPages})`}
        </p>
      </div>

      {/* Comics grid */}
      <div className="comics-grid">
        {safeComics.map(comic => (
          <ComicCard
            key={comic.id}
            comic={comic}
            onEdit={onEdit}
            onDelete={onDelete}
          />
        ))}
      </div>

      {/* Pagination */}
      {totalPages > 1 && (
        <div className="d-flex justify-content-center align-items-center gap-2 mt-4">
          {generatePaginationButtons()}
        </div>
      )}
    </div>
  );
};

export default ComicList;