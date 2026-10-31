import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { comicService } from '../services/comicService';
import ComicForm from '../components/Comics/ComicForm';
import ComicVineSearch from '../components/Comics/ComicVineSearch';
import ErrorMessage from '../components/Common/ErrorMessage';
import '../styles/pages/AddComicPage.css';

const AddComicPage = () => {
  const navigate = useNavigate();
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');
  const [activeTab, setActiveTab] = useState('search');

  const handleSave = async (data) => {
    setSaving(true);
    setError('');

    try {
      const response = await comicService.createComic(data);
      
      if (response.success) {
        navigate('/collection');
      } else {
        setError(response.message);
      }
    } catch (err) {
      setError('Failed to add comic. Please try again.');
    } finally {
      setSaving(false);
    }
  };

  const handleCancel = () => {
    navigate('/collection');
  };

  const handleImportSuccess = () => {
    navigate('/collection');
  };

  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>Add New Comic</h1>
          <p className="text-muted">Search the ComicVine database or enter details manually</p>
        </div>

        {error && <ErrorMessage message={error} onDismiss={() => setError('')} />}

        <div className="add-comic-tabs">
          <button
            type="button"
            className={`tab-button ${activeTab === 'search' ? 'active' : ''}`}
            onClick={() => setActiveTab('search')}
          >
            Search ComicVine
          </button>
          <button
            type="button"
            className={`tab-button ${activeTab === 'manual' ? 'active' : ''}`}
            onClick={() => setActiveTab('manual')}
          >
            Manual Entry
          </button>
        </div>

        <div className="tab-content">
          {activeTab === 'search' ? (
            <ComicVineSearch onImportSuccess={handleImportSuccess} />
          ) : (
            <ComicForm
              onSave={handleSave}
              onCancel={handleCancel}
              loading={saving}
            />
          )}
        </div>
      </div>
    </div>
  );
};

export default AddComicPage;
