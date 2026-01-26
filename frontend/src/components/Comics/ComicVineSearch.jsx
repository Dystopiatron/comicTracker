import React, { useState, useCallback, useEffect } from 'react';
import { 
  searchComicVine, 
  searchIssues, 
  getIssue, 
  importToCollection,
  getComicVineStatus,
  ResourceTypes 
} from '../../services/comicVineService';
import './ComicVineSearch.css';

/**
 * ComicVine Search Component
 * Allows users to search the ComicVine database and import comics to their collection
 */
const ComicVineSearch = ({ onImportSuccess }) => {
  const [query, setQuery] = useState('');
  const [resourceType, setResourceType] = useState('Issue');
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const [totalResults, setTotalResults] = useState(0);
  
  // ComicVine API status
  const [apiStatus, setApiStatus] = useState({ isConfigured: true, message: '' });
  const [checkingStatus, setCheckingStatus] = useState(true);
  
  // Import modal state
  const [showImportModal, setShowImportModal] = useState(false);
  const [selectedIssue, setSelectedIssue] = useState(null);
  const [issueDetails, setIssueDetails] = useState(null);
  const [loadingDetails, setLoadingDetails] = useState(false);
  const [importData, setImportData] = useState({
    condition: 'VeryFine',
    purchasePrice: '',
    notes: ''
  });
  const [importing, setImporting] = useState(false);

  const pageSize = 10;

  // Check API status on mount
  useEffect(() => {
    const checkStatus = async () => {
      setCheckingStatus(true);
      const status = await getComicVineStatus();
      setApiStatus(status);
      setCheckingStatus(false);
    };
    checkStatus();
  }, []);

  const handleSearch = useCallback(async (searchPage = 1) => {
    if (!query.trim()) {
      setError('Please enter a search term');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const response = resourceType === 'Issue' 
        ? await searchIssues(query, searchPage, pageSize)
        : await searchComicVine(query, { resourceType, page: searchPage, pageSize });

      if (response.success) {
        setResults(response.data.results || []);
        setTotalPages(response.data.totalPages || 0);
        setTotalResults(response.data.totalResults || 0);
        setPage(searchPage);
      } else {
        setError(response.message || 'Search failed');
        setResults([]);
      }
    } catch (err) {
      // Check for 503 Service Unavailable (API not configured)
      if (err.response?.status === 503) {
        setApiStatus({ isConfigured: false, message: err.response.data?.message || 'ComicVine API is not configured' });
      } else {
        setError(err.response?.data?.message || 'An error occurred while searching');
      }
      setResults([]);
    } finally {
      setLoading(false);
    }
  }, [query, resourceType]);

  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      handleSearch(1);
    }
  };

  const handleSelectIssue = async (issue) => {
    setSelectedIssue(issue);
    setShowImportModal(true);
    setLoadingDetails(true);
    setIssueDetails(null);

    try {
      const response = await getIssue(issue.id);
      if (response.success) {
        setIssueDetails(response.data);
      }
    } catch (err) {
      console.error('Error fetching issue details:', err);
    } finally {
      setLoadingDetails(false);
    }
  };

  const handleImport = async () => {
    if (!selectedIssue) return;

    setImporting(true);
    setError(null);

    try {
      const response = await importToCollection({
        comicVineIssueId: selectedIssue.id,
        condition: importData.condition,
        purchasePrice: importData.purchasePrice ? parseFloat(importData.purchasePrice) : null,
        notes: importData.notes || null
      });

      console.log('Import response:', response);

      if (response.success) {
        setShowImportModal(false);
        setSelectedIssue(null);
        setIssueDetails(null);
        setImportData({ condition: 'VeryFine', purchasePrice: '', notes: '' });
        
        if (onImportSuccess) {
          onImportSuccess(response.data);
        }
        
        alert('Comic added to your collection successfully!');
      } else {
        setError(response.message || 'Failed to import comic. Please make sure you are logged in.');
      }
    } catch (err) {
      console.error('Import error:', err);
      setError(err.response?.data?.message || 'An error occurred while importing. Please make sure you are logged in.');
    } finally {
      setImporting(false);
    }
  };

  const closeModal = () => {
    setShowImportModal(false);
    setSelectedIssue(null);
    setIssueDetails(null);
    setImportData({ condition: 'VeryFine', purchasePrice: '', notes: '' });
  };

  const conditionOptions = [
    { value: 'Mint', label: 'Mint (M)' },
    { value: 'NearMint', label: 'Near Mint (NM)' },
    { value: 'VeryFine', label: 'Very Fine (VF)' },
    { value: 'Fine', label: 'Fine (F)' },
    { value: 'VeryGood', label: 'Very Good (VG)' },
    { value: 'Good', label: 'Good (G)' },
    { value: 'Fair', label: 'Fair (FR)' },
    { value: 'Poor', label: 'Poor (P)' }
  ];

  // Show loading state while checking API status
  if (checkingStatus) {
    return (
      <div className="comicvine-search">
        <div className="search-header">
          <h2>Search ComicVine Database</h2>
          <p>Checking API availability...</p>
        </div>
      </div>
    );
  }

  // Show message if API is not configured
  if (!apiStatus.isConfigured) {
    return (
      <div className="comicvine-search">
        <div className="search-header">
          <h2>Search ComicVine Database</h2>
        </div>
        <div className="api-not-configured">
          <div className="info-icon">ℹ️</div>
          <h3>ComicVine Integration Not Configured</h3>
          <p>
            The ComicVine API integration is an optional feature that allows you to search and import 
            comics from the ComicVine database.
          </p>
          <p>
            To enable this feature, you'll need to:
          </p>
          <ol>
            <li>Get a free API key from <a href="https://comicvine.gamespot.com/api/" target="_blank" rel="noopener noreferrer">comicvine.gamespot.com/api</a></li>
            <li>Add the API key to the backend configuration</li>
          </ol>
          <p className="note">
            You can still add comics manually to your collection without this feature.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="comicvine-search">
      <div className="search-header">
        <h2>Search ComicVine Database</h2>
        <p>Find comics in the ComicVine database and add them to your collection</p>
      </div>

      <div className="search-controls">
        <div className="search-input-group">
          <input
            type="text"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            onKeyPress={handleKeyPress}
            placeholder="Search for comics, characters, volumes..."
            className="search-input"
          />
          <select
            value={resourceType}
            onChange={(e) => setResourceType(e.target.value)}
            className="resource-select"
          >
            {Object.entries(ResourceTypes).map(([key, value]) => (
              <option key={key} value={value}>{value}</option>
            ))}
          </select>
          <button 
            onClick={() => handleSearch(1)} 
            disabled={loading}
            className="search-button"
          >
            {loading ? 'Searching...' : 'Search'}
          </button>
        </div>
      </div>

      {error && (
        <div className="error-message">
          {error}
        </div>
      )}

      {totalResults > 0 && (
        <div className="results-info">
          Found {totalResults} results (Page {page} of {totalPages})
        </div>
      )}

      <div className="search-results">
        {loading ? (
          <div className="loading-spinner">
            <div className="spinner"></div>
            <p>Searching ComicVine...</p>
          </div>
        ) : results.length > 0 ? (
          <div className="results-grid">
            {results.map((result) => (
              <div key={result.id} className="result-card">
                <div className="result-image">
                  {result.imageUrl ? (
                    <img src={result.imageUrl} alt={result.name} />
                  ) : (
                    <div className="no-image">No Image</div>
                  )}
                </div>
                <div className="result-info">
                  <h3>{result.name || 'Unknown'}</h3>
                  <span className="resource-type-badge">{result.resourceType}</span>
                  {result.description && (
                    <p className="result-description">
                      {result.description.substring(0, 150)}
                      {result.description.length > 150 ? '...' : ''}
                    </p>
                  )}
                  <div className="result-actions">
                    {result.resourceType === 'issue' && (
                      <button 
                        onClick={() => handleSelectIssue(result)}
                        className="add-button"
                      >
                        Add to Collection
                      </button>
                    )}
                    {result.siteDetailUrl && (
                      <a 
                        href={result.siteDetailUrl} 
                        target="_blank" 
                        rel="noopener noreferrer"
                        className="view-link"
                      >
                        View on ComicVine
                      </a>
                    )}
                  </div>
                </div>
              </div>
            ))}
          </div>
        ) : query && !loading ? (
          <div className="no-results">
            <p>No results found for "{query}"</p>
            <p>Try a different search term or resource type</p>
          </div>
        ) : null}
      </div>

      {/* Pagination */}
      {totalPages > 1 && (
        <div className="pagination">
          <button
            onClick={() => handleSearch(page - 1)}
            disabled={page <= 1 || loading}
            className="pagination-button"
          >
            Previous
          </button>
          <span className="page-info">Page {page} of {totalPages}</span>
          <button
            onClick={() => handleSearch(page + 1)}
            disabled={page >= totalPages || loading}
            className="pagination-button"
          >
            Next
          </button>
        </div>
      )}

      {/* Import Modal */}
      {showImportModal && (
        <div className="modal-overlay" onClick={closeModal}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <div className="modal-header">
              <h3>Add to Collection</h3>
              <button className="close-button" onClick={closeModal}>&times;</button>
            </div>
            
            <div className="modal-body">
              {loadingDetails ? (
                <div className="loading-details">Loading issue details...</div>
              ) : issueDetails ? (
                <>
                  <div className="issue-preview">
                    {issueDetails.coverImageUrl && (
                      <img 
                        src={issueDetails.coverImageUrl} 
                        alt={issueDetails.name || 'Comic cover'} 
                        className="preview-image"
                      />
                    )}
                    <div className="issue-info">
                      <h4>{issueDetails.volume?.name || 'Unknown Series'}</h4>
                      <p className="issue-number">Issue #{issueDetails.issueNumber}</p>
                      {issueDetails.coverDate && (
                        <p className="cover-date">
                          Cover Date: {new Date(issueDetails.coverDate).toLocaleDateString()}
                        </p>
                      )}
                      {issueDetails.volume?.publisher && (
                        <p className="publisher">Publisher: {issueDetails.volume.publisher.name}</p>
                      )}
                    </div>
                  </div>

                  <div className="import-form">
                    <div className="form-group">
                      <label htmlFor="condition">Condition *</label>
                      <select
                        id="condition"
                        value={importData.condition}
                        onChange={(e) => setImportData({...importData, condition: e.target.value})}
                      >
                        {conditionOptions.map(opt => (
                          <option key={opt.value} value={opt.value}>{opt.label}</option>
                        ))}
                      </select>
                    </div>

                    <div className="form-group">
                      <label htmlFor="purchasePrice">Purchase Price ($)</label>
                      <input
                        type="number"
                        id="purchasePrice"
                        value={importData.purchasePrice}
                        onChange={(e) => setImportData({...importData, purchasePrice: e.target.value})}
                        placeholder="0.00"
                        min="0"
                        step="0.01"
                      />
                    </div>

                    <div className="form-group">
                      <label htmlFor="notes">Notes</label>
                      <textarea
                        id="notes"
                        value={importData.notes}
                        onChange={(e) => setImportData({...importData, notes: e.target.value})}
                        placeholder="Any notes about this comic..."
                        rows="3"
                      />
                    </div>
                  </div>
                </>
              ) : (
                <p>Unable to load issue details</p>
              )}
            </div>

            <div className="modal-footer">
              <button className="cancel-button" onClick={closeModal}>
                Cancel
              </button>
              <button 
                className="import-button" 
                onClick={handleImport}
                disabled={importing || !issueDetails}
              >
                {importing ? 'Adding...' : 'Add to Collection'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ComicVineSearch;
