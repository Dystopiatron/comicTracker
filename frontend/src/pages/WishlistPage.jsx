import React, { useState } from 'react';
import { useWishlist } from '../hooks/useWishlist';
import { wishlistService } from '../services/wishlistService';
import WishlistSearch from '../components/Wishlist/WishlistSearch';
import WishlistList from '../components/Wishlist/WishlistList';
import WishlistForm from '../components/Wishlist/WishlistForm';
import ErrorMessage from '../components/Common/ErrorMessage';

const WishlistPage = () => {
  const {
    wishlistItems,
    loading,
    error,
    totalCount,
    currentPage,
    totalPages,
    filters,
    updateFilters,
    goToPage,
    refreshWishlist
  } = useWishlist();

  const [showAddForm, setShowAddForm] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [actionError, setActionError] = useState('');
  const [actionSuccess, setActionSuccess] = useState('');
  const [actionLoading, setActionLoading] = useState(false);

  const handleAddItem = () => {
    setShowAddForm(true);
    setEditingItem(null);
  };

  const handleEditItem = (item) => {
    setEditingItem(item);
    setShowAddForm(true);
  };

  const handleCancelForm = () => {
    setShowAddForm(false);
    setEditingItem(null);
  };

  const handleSaveItem = async (itemData) => {
    try {
      setActionError('');
      setActionLoading(true);

      let response;
      if (editingItem) {
        response = await wishlistService.updateWishlistItem(editingItem.id, itemData);
      } else {
        response = await wishlistService.createWishlistItem(itemData);
      }

      if (response.success) {
        setActionSuccess(editingItem ? 'Wishlist item updated successfully!' : 'Item added to wishlist!');
        setShowAddForm(false);
        setEditingItem(null);
        refreshWishlist();
        
        // Clear success message after 3 seconds
        setTimeout(() => setActionSuccess(''), 3000);
      } else {
        setActionError(response.message);
      }
    } catch (err) {
      setActionError('Failed to save wishlist item. Please try again.');
    } finally {
      setActionLoading(false);
    }
  };

  const handleDeleteItem = async (id) => {
    try {
      setActionError('');
      const response = await wishlistService.deleteWishlistItem(id);

      if (response.success) {
        setActionSuccess('Item removed from wishlist');
        refreshWishlist();
        setTimeout(() => setActionSuccess(''), 3000);
      } else {
        setActionError(response.message);
      }
    } catch (err) {
      setActionError('Failed to delete wishlist item. Please try again.');
    }
  };

  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>My Wishlist</h1>
          <p className="text-muted">Track comics you want to acquire</p>
        </div>

        {actionError && <ErrorMessage message={actionError} onDismiss={() => setActionError('')} />}
        {actionSuccess && <ErrorMessage message={actionSuccess} type="success" onDismiss={() => setActionSuccess('')} />}

        {showAddForm ? (
          <WishlistForm
            item={editingItem}
            onSave={handleSaveItem}
            onCancel={handleCancelForm}
            loading={actionLoading}
          />
        ) : (
          <>
            <div className="mb-4">
              <WishlistSearch
                filters={filters}
                onFilterChange={updateFilters}
                onAddItem={handleAddItem}
              />
            </div>

            <WishlistList
              wishlistItems={wishlistItems}
              loading={loading}
              error={error}
              currentPage={currentPage}
              totalPages={totalPages}
              totalCount={totalCount}
              onEdit={handleEditItem}
              onDelete={handleDeleteItem}
              onPageChange={goToPage}
            />
          </>
        )}
      </div>
    </div>
  );
};

export default WishlistPage;
