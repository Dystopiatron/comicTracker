import React, { useState } from 'react';
import { PUBLISHERS } from '../../types';
import Button from '../Common/Button';
import { debounce } from '../../utils/helpers';

const ComicSearch = ({ filters, onFilterChange, onAddComic }) => {
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

  const handleConditionChange = (e) => {
    const value = e.target.value;
    onFilterChange({
      ...filters,
      condition: value ? parseInt(value) : undefined
    });
  };

  const handleClearFilters = () => {
    setSearchTerm('');
    onFilterChange({});
  };

  const hasActiveFilters = filters.searchTerm || filters.publisher || filters.condition;

  return (
    <div className="card">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h3 style={{ margin: 0 }}>Search & Filter Comics</h3>
        <Button variant="primary" onClick={onAddComic}>
          Add New Comic
        </Button>
      </div>

      <div className="d-flex gap-3 flex-wrap">
        <div className="form-group" style={{ flex: '2', minWidth: '200px' }}>
          <label htmlFor="search" className="form-label">Search Comics</label>
          <input
            type="text"
            id="search"
            className="form-control"
            placeholder="Search by series name, issue number..."
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </div>

        <div className="form-group" style={{ flex: '1', minWidth: '150px' }}>
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

        <div className="form-group" style={{ flex: '1', minWidth: '150px' }}>
          <label htmlFor="condition" className="form-label">Condition</label>
          <select
            id="condition"
            className="form-control form-select"
            value={filters.condition || ''}
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

        <div className="d-flex align-items-end">
          {hasActiveFilters && (
            <Button
              variant="outline"
              onClick={handleClearFilters}
              style={{ marginBottom: '1rem' }}
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
            {filters.condition && ` Condition: ${filters.condition}`}
          </small>
        </div>
      )}
    </div>
  );
};

export default ComicSearch;