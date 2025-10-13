import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useComics } from '../hooks/useComics';
import { comicService } from '../services/comicService';
import ComicSearch from '../components/Comics/ComicSearch';
import ComicList from '../components/Comics/ComicList';
import ErrorMessage from '../components/Common/ErrorMessage';

const CollectionPage = () => {
  const navigate = useNavigate();
  const {
    comics,
    loading,
    error,
    totalCount,
    currentPage,
    totalPages,
    filters,
    updateFilters,
    goToPage,
    refreshComics
  } = useComics();
  const [deleteError, setDeleteError] = useState('');

  const handleAddComic = () => {
    navigate('/add-comic');
  };

  const handleEditComic = (comic) => {
    navigate(`/edit-comic/${comic.id}`, { state: { comic } });
  };

  const handleDeleteComic = async (id) => {
    try {
      setDeleteError('');
      const response = await comicService.deleteComic(id);
      
      if (response.success) {
        refreshComics();
      } else {
        setDeleteError(response.message);
      }
    } catch (err) {
      setDeleteError('Failed to delete comic. Please try again.');
    }
  };

  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>My Comic Collection</h1>
          <p className="text-muted">Manage and organize your comic book collection</p>
        </div>

        {deleteError && <ErrorMessage message={deleteError} onDismiss={() => setDeleteError('')} />}

        <div className="mb-4">
          <ComicSearch
            filters={filters}
            onFilterChange={updateFilters}
            onAddComic={handleAddComic}
          />
        </div>

        <ComicList
          comics={comics}
          loading={loading}
          error={error}
          currentPage={currentPage}
          totalPages={totalPages}
          totalCount={totalCount}
          onEdit={handleEditComic}
          onDelete={handleDeleteComic}
          onPageChange={goToPage}
        />
      </div>
    </div>
  );
};

export default CollectionPage;