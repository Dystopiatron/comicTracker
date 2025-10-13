import { useState, useEffect, useCallback } from 'react';
import { comicService } from '../services/comicService';

export const useComics = (initialPage = 1, initialPageSize = 10) => {
  const [comics, setComics] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [totalCount, setTotalCount] = useState(0);
  const [currentPage, setCurrentPage] = useState(initialPage);
  const [totalPages, setTotalPages] = useState(0);
  const [filters, setFilters] = useState({});

  const fetchComics = useCallback(async (page = currentPage, pageSize = initialPageSize, newFilters) => {
    setLoading(true);
    setError(null);
    
    try {
      const filtersToUse = newFilters !== undefined ? newFilters : filters;
      const response = await comicService.getComics(page, pageSize, filtersToUse);
      
      if (response.success && response.data) {
        // Handle double-wrapped response: response.data.data.items
        const actualData = response.data.data || response.data;
        setComics(actualData.items || []);
        setTotalCount(actualData.totalCount || 0);
        setCurrentPage(actualData.page || page);
        setTotalPages(actualData.totalPages || 0);
      } else {
        setError(response.message);
        setComics([]);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to fetch comics');
      setComics([]);
    } finally {
      setLoading(false);
    }
  }, [currentPage, initialPageSize, filters]);

  const updateFilters = (newFilters) => {
    setFilters(newFilters);
    setCurrentPage(1); // Reset to first page when filters change
    fetchComics(1, initialPageSize, newFilters);
  };

  const goToPage = (page) => {
    if (page >= 1 && page <= totalPages) {
      setCurrentPage(page);
      fetchComics(page, initialPageSize);
    }
  };

  const refreshComics = () => {
    fetchComics(currentPage, initialPageSize);
  };

  useEffect(() => {
    fetchComics();
  }, [fetchComics]); // Initial load

  return {
    comics,
    loading,
    error,
    totalCount,
    currentPage,
    totalPages,
    filters,
    updateFilters,
    goToPage,
    refreshComics,
    fetchComics
  };
};
