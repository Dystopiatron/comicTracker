import React, { useState } from 'react';
import { PUBLISHERS, WISHLIST_PRIORITIES, WISHLIST_SORT_OPTIONS } from '../../types';
import Button from '../Common/Button';
import { debounce } from '../../utils/helpers';

const WishlistSearch = ({ filters, onFilterChange, onAddItem }) => {
  const [searchTerm, setSearchTerm] = useState(filters.searchTerm || '');

  // Debounce search to avoid too many API calls
  const debouncedSearch = debounce((term) => {
    onFilterChange({
      ...filters,
      searchTerm: term || undefined
    });
  }, 500);

  const handleSearchChange = (e) => {
    const value = e.target.value;
    setSearchTerm(value);
    debouncedSearch(value);
  };

  const handlePublisherChange = (e) => {
    const value = e.target.value;
    onFilterChange({
      ...filters,
      publisher: value || undefined
    });
  };

  const handlePriorityChange = (e) => {
    const value = e.target.value;
    onFilterChange({
      ...filters,
      priority: value ? parseInt(value) : undefined
    });
  };

  const handleConditionChange = (e) => {
    const value = e.target.value;
    onFilterChange({
      ...filters,
      desiredCondition: value ? parseInt(value) : undefined
    });
  };

  const handleSortByChange = (e) => {
    const value = e.target.value;
    onFilterChange({
      ...filters,
      sortBy: value || 'priority',
      sortOrder: filters.sortOrder || 'asc'
    });
  };

  const handleSortOrderChange = (e) => {
    const value = e.target.value;
    onFilterChange({
      ...filters,
      sortBy: filters.sortBy || 'priority',
      sortOrder: value
    });
  };

  const handleClearFilters = () => {
    setSearchTerm('');
    onFilterChange({});
  };

  const hasActiveFilters = filters.searchTerm || filters.publisher || filters.priority || filters.desiredCondition || filters.sortBy;

  return (
    <div className="card">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h3 className="section-header">Search & Filter Wishlist</h3>
        <Button variant="primary" onClick={onAddItem}>
          Add to Wishlist
        </Button>
      </div>

      <div className="search-form-row">
        <div className="form-group form-group-wide">
          <label htmlFor="search" className="form-label">Search Wishlist</label>
          <input
            type="text"
            id="search"
            className="form-control"
            placeholder="Search by series name, issue number, publisher..."
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </div>

        <div className="form-group form-group-narrow">
          <label htmlFor="publisher" className="form-label">Publisher</label>
          <select
            id="publisher"
            className="form-control form-select"
            value={filters.publisher || ''}
            onChange={handlePublisherChange}
          >
            <option value="">All Publishers</option>
            {PUBLISHERS.map(publisher => (
              <option key={publisher} value={publisher}>
                {publisher}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group form-group-narrow">
          <label htmlFor="priority" className="form-label">Priority</label>
          <select
            id="priority"
            className="form-control form-select"
            value={filters.priority || ''}
            onChange={handlePriorityChange}
          >
            <option value="">All Priorities</option>
            {WISHLIST_PRIORITIES.map(priority => (
              <option key={priority.value} value={priority.value}>
                {priority.label}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group form-group-narrow">
          <label htmlFor="condition" className="form-label">Condition</label>
          <select
            id="condition"
            className="form-control form-select"
            value={filters.desiredCondition || ''}
            onChange={handleConditionChange}
          >
            <option value="">All Conditions</option>
            <option value="1">Poor</option>
            <option value="2">Fair</option>
            <option value="3">Good</option>
            <option value="4">Fine</option>
            <option value="5">Very Fine</option>
            <option value="6">Near Mint</option>
            <option value="7">Mint</option>
          </select>
        </div>
      </div>

      <div className="search-form-row">
        <div className="form-group form-group-flex">
          <label htmlFor="sortBy" className="form-label">Sort By</label>
          <select
            id="sortBy"
            className="form-control form-select"
            value={filters.sortBy || 'priority'}
            onChange={handleSortByChange}
          >
            {WISHLIST_SORT_OPTIONS.map(option => (
              <option key={option.value} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group form-group-flex">
          <label htmlFor="sortOrder" className="form-label">Order</label>
          <select
            id="sortOrder"
            className="form-control form-select"
            value={filters.sortOrder || 'asc'}
            onChange={handleSortOrderChange}
          >
            <option value="asc">Ascending</option>
            <option value="desc">Descending</option>
          </select>
        </div>

        <div className="d-flex align-items-end">
          {hasActiveFilters && (
            <Button
              variant="outline"
              onClick={handleClearFilters}
              className="mb-4"
            >
              Clear Filters
            </Button>
          )}
        </div>
      </div>

      {hasActiveFilters && (
        <div className="mt-2">
          <small className="text-muted">
            Active filters: 
            {filters.searchTerm && ` Search: "${filters.searchTerm}"`}
            {filters.publisher && ` Publisher: ${filters.publisher}`}
            {filters.priority && ` Priority: ${WISHLIST_PRIORITIES.find(p => p.value === filters.priority)?.label}`}
            {filters.desiredCondition && ` Condition: ${['', 'Poor', 'Fair', 'Good', 'Fine', 'Very Fine', 'Near Mint', 'Mint'][filters.desiredCondition]}`}
            {filters.sortBy && ` Sort: ${WISHLIST_SORT_OPTIONS.find(s => s.value === filters.sortBy)?.label} (${filters.sortOrder})`}
          </small>
        </div>
      )}
    </div>
  );
};

export default WishlistSearch;
