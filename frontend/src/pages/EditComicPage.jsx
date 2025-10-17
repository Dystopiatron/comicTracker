import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { comicService } from '../services/comicService';
import ComicForm from '../components/Comics/ComicForm';
import LoadingSpinner from '../components/Common/LoadingSpinner';
import ErrorMessage from '../components/Common/ErrorMessage';

const EditComicPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const location = useLocation();
  const [comic, setComic] = useState(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchComic = async () => {
      try {
        setLoading(true);
        setError('');

        // Try to get comic from location state first
        if (location.state?.comic) {
          setComic(location.state.comic);
          setLoading(false);
          return;
        }

        // Otherwise fetch from API
        if (!id) {
          setError('Comic ID is required');
          setLoading(false);
          return;
        }

        const response = await comicService.getComic(id);
        if (response.success && response.data) {
          setComic(response.data);
        } else {
          setError(response.message || 'Failed to load comic');
        }
      } catch (err) {
        setError('Failed to load comic');
      } finally {
        setLoading(false);
      }
    };

    fetchComic();
  }, [id, location.state]);

  const handleSave = async (data) => {
    try {
      setSaving(true);
      setError('');

      const response = await comicService.updateComic(id, data);
      if (response.success) {
        navigate('/collection');
      } else {
        setError(response.message || 'Failed to update comic');
      }
    } catch (err) {
      setError('Failed to update comic');
    } finally {
      setSaving(false);
    }
  };

  if (loading) {
    return (
      <div className="container">
        <div className="text-center loading-section">
          <LoadingSpinner size="lg" />
          <p className="mt-2 text-muted">Loading comic...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>Edit Comic</h1>
          <p className="text-muted">
            {comic && `Editing: ${comic.seriesName} ${comic.issueNumber?.startsWith('#') ? comic.issueNumber : `#${comic.issueNumber || ''}`}`}
          </p>
        </div>

        {error && <ErrorMessage message={error} onDismiss={() => setError('')} />}

        {comic && (
          <ComicForm
            comic={comic}
            onSave={handleSave}
            loading={saving}
            onCancel={() => navigate('/collection')}
          />
        )}
      </div>
    </div>
  );
};

export default EditComicPage;