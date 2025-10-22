import { useState, useEffect, useCallback } from 'react';
import { wishlistService } from '../services/wishlistService';

export const useWishlist = (initialPage = 1, initialPageSize = 10) => {
  const [wishlistItems, setWishlistItems] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [totalCount, setTotalCount] = useState(0);
  const [currentPage, setCurrentPage] = useState(initialPage);
  const [totalPages, setTotalPages] = useState(0);
  const [filters, setFilters] = useState({});

  const fetchWishlist = useCallback(async (page = currentPage, pageSize = initialPageSize, newFilters) => {
    setLoading(true);
    setError(null);
    
    try {
      const filtersToUse = newFilters !== undefined ? newFilters : filters;
      const response = await wishlistService.getWishlist(page, pageSize, filtersToUse);
      
      if (response.success && response.data) {
        // Handle double-wrapped response: response.data.data.items
        const actualData = response.data.data || response.data;
        setWishlistItems(actualData.items || []);
        setTotalCount(actualData.totalCount || 0);
        setCurrentPage(actualData.page || page);
        setTotalPages(actualData.totalPages || 0);
      } else {
        setError(response.message);
        setWishlistItems([]);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to fetch wishlist');
      setWishlistItems([]);
    } finally {
      setLoading(false);
    }
  }, [currentPage, initialPageSize, filters]);

  const updateFilters = (newFilters) => {
    setFilters(newFilters);
    setCurrentPage(1); // Reset to first page when filters change
    fetchWishlist(1, initialPageSize, newFilters);
  };

  const goToPage = (page) => {
    if (page >= 1 && page <= totalPages) {
      setCurrentPage(page);
      fetchWishlist(page, initialPageSize);
    }
  };

  const refreshWishlist = () => {
    fetchWishlist(currentPage, initialPageSize);
  };

  useEffect(() => {
    fetchWishlist();
  }, [fetchWishlist]); // Initial load

  return {
    wishlistItems,
    loading,
    error,
    totalCount,
    currentPage,
    totalPages,
    filters,
    updateFilters,
    goToPage,
    refreshWishlist,
    fetchWishlist
  };
};
