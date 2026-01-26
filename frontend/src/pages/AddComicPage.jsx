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
  const [activeTab, setActiveTab] = useState('search'); // 'search' or 'manual'
  const [prefilledComic, setPrefilledComic] = useState(null);

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

  // Called when a comic is successfully imported from ComicVine
  const handleImportSuccess = (comic) => {
    navigate('/collection');
  };

  // Called when user wants to use ComicVine data but customize it first
  const handleUseComicVineData = (issueData) => {
    // Map ComicVine data to form format
    const mappedData = {
      seriesName: issueData.volume?.name || issueData.name || '',
      issueNumber: issueData.issueNumber || '',
      publisher: issueData.volume?.publisher?.name || '',
      coverImageUrl: issueData.coverImageUrl || issueData.smallImageUrl || '',
      notes: issueData.description ? `ComicVine ID: ${issueData.id}\n${issueData.description.substring(0, 200)}...` : `ComicVine ID: ${issueData.id}`,
      condition: 3, // Default to Good
      purchasePrice: undefined
    };
    setPrefilledComic(mappedData);
    setActiveTab('manual');
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
            className={`tab-button ${activeTab === 'search' ? 'active' : ''}`}
            onClick={() => setActiveTab('search')}
          >
            🔍 Search ComicVine
          </button>
          <button 
            className={`tab-button ${activeTab === 'manual' ? 'active' : ''}`}
            onClick={() => setActiveTab('manual')}
          >
            ✏️ Manual Entry
          </button>
        </div>

        <div className="tab-content">
          {activeTab === 'search' ? (
            <ComicVineSearch 
              onImportSuccess={handleImportSuccess}
            />
          ) : (
            <ComicForm
              comic={prefilledComic}
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