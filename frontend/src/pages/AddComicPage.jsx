import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { comicService } from '../services/comicService';
import ComicForm from '../components/Comics/ComicForm';
import ErrorMessage from '../components/Common/ErrorMessage';

const AddComicPage = () => {
  const navigate = useNavigate();
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');

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

  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>Add New Comic</h1>
          <p className="text-muted">Add a comic to your collection</p>
        </div>

        {error && <ErrorMessage message={error} onDismiss={() => setError('')} />}

        <ComicForm
          onSave={handleSave}
          onCancel={handleCancel}
          loading={saving}
        />
      </div>
    </div>
  );
};

export default AddComicPage;