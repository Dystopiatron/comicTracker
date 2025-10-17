import React, { useState, useEffect } from 'react';
import { COMIC_CONDITIONS, PUBLISHERS } from '../../types';
import Button from '../Common/Button';
import ErrorMessage from '../Common/ErrorMessage';

const ComicForm = ({ comic, onSave, onCancel, loading = false }) => {
  const [formData, setFormData] = useState({
    seriesName: '',
    issueNumber: '',
    publisher: '',
    condition: 3, // Default to "Good"
    purchasePrice: undefined,
    coverImageUrl: '',
    notes: ''
  });
  const [errors, setErrors] = useState({});
  const [submitError, setSubmitError] = useState('');

  // Initialize form data when comic prop changes
  useEffect(() => {
    if (comic) {
      // Convert condition from string to numeric value if needed
      let conditionValue = comic.condition;
      if (typeof comic.condition === 'string') {
        const conditionMapping = {
          'Poor': 1,
          'Fair': 2, 
          'Good': 3,
          'Fine': 4,
          'VeryFine': 5,
          'Very Fine': 5,
          'NearMint': 6,
          'Near Mint': 6,
          'Mint': 7
        };
        conditionValue = conditionMapping[comic.condition] || 3; // Default to Good
      }

      setFormData({
        seriesName: comic.seriesName || '',
        issueNumber: comic.issueNumber || '',
        publisher: comic.publisher || '',
        condition: conditionValue,
        purchasePrice: comic.purchasePrice,
        coverImageUrl: comic.coverImageUrl || '',
        notes: comic.notes || ''
      });
    }
  }, [comic]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    let processedValue = value;

    // Handle numeric fields
    if (name === 'condition') {
      processedValue = parseInt(value);
    } else if (name === 'purchasePrice') {
      processedValue = value ? parseFloat(value) : undefined;
    }

    setFormData(prev => ({
      ...prev,
      [name]: processedValue
    }));

    // Clear error for this field when user starts typing
    if (errors[name]) {
      setErrors(prev => ({
        ...prev,
        [name]: ''
      }));
    }
  };

  const validateForm = () => {
    const newErrors = {};

    if (!formData.seriesName.trim()) {
      newErrors.seriesName = 'Series name is required';
    }

    if (!formData.issueNumber.trim()) {
      newErrors.issueNumber = 'Issue number is required';
    }

    if (!formData.publisher) {
      newErrors.publisher = 'Publisher is required';
    }

    if (formData.purchasePrice !== undefined && formData.purchasePrice < 0) {
      newErrors.purchasePrice = 'Price cannot be negative';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    try {
      setSubmitError('');
      
      // Clean up the data before sending
      const cleanedData = {
        ...formData,
        coverImageUrl: formData.coverImageUrl.trim() || null,
        notes: formData.notes.trim() || null
      };
      
      await onSave(cleanedData);
    } catch (error) {
      setSubmitError(error instanceof Error ? error.message : 'Failed to save comic');
    }
  };

  return (
    <div className="card">
      <div className="card-header">
        <h2>{comic ? 'Edit Comic' : 'Add New Comic'}</h2>
      </div>

      {submitError && <ErrorMessage message={submitError} onDismiss={() => setSubmitError('')} />}

      <form onSubmit={handleSubmit}>
        <div className="form-row">
          <div className="form-group form-group-flex-2">
            <label htmlFor="seriesName" className="form-label">Series Name *</label>
            <input
              type="text"
              id="seriesName"
              name="seriesName"
              className="form-control"
              value={formData.seriesName}
              onChange={handleInputChange}
              disabled={loading}
              placeholder="e.g., Amazing Spider-Man"
            />
            {errors.seriesName && <div className="form-error">{errors.seriesName}</div>}
          </div>

          <div className="form-group form-group-flex">
            <label htmlFor="issueNumber" className="form-label">Issue Number *</label>
            <input
              type="text"
              id="issueNumber"
              name="issueNumber"
              className="form-control"
              value={formData.issueNumber}
              onChange={handleInputChange}
              disabled={loading}
              placeholder="e.g., 1, 15, Annual 1"
            />
            {errors.issueNumber && <div className="form-error">{errors.issueNumber}</div>}
          </div>
        </div>

        <div className="form-row">
          <div className="form-group form-group-flex">
            <label htmlFor="publisher" className="form-label">Publisher *</label>
            <select
              id="publisher"
              name="publisher"
              className="form-control form-select"
              value={formData.publisher}
              onChange={handleInputChange}
              disabled={loading}
            >
              <option value="">Select Publisher</option>
              {PUBLISHERS.map(publisher => (
                <option key={publisher} value={publisher}>
                  {publisher}
                </option>
              ))}
            </select>
            {errors.publisher && <div className="form-error">{errors.publisher}</div>}
          </div>

          <div className="form-group form-group-flex">
            <label htmlFor="condition" className="form-label">Condition</label>
            <select
              id="condition"
              name="condition"
              className="form-control form-select"
              value={formData.condition}
              onChange={handleInputChange}
              disabled={loading}
            >
              {COMIC_CONDITIONS.map(condition => (
                <option key={condition.value} value={condition.value}>
                  {condition.label}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group form-group-flex">
            <label htmlFor="purchasePrice" className="form-label">Purchase Price</label>
            <input
              type="number"
              id="purchasePrice"
              name="purchasePrice"
              className="form-control"
              value={formData.purchasePrice || ''}
              onChange={handleInputChange}
              disabled={loading}
              min="0"
              step="0.01"
              placeholder="0.00"
            />
            {errors.purchasePrice && <div className="form-error">{errors.purchasePrice}</div>}
          </div>
        </div>

        <div className="form-group">
          <label htmlFor="coverImageUrl" className="form-label">Cover Image URL</label>
          <input
            type="url"
            id="coverImageUrl"
            name="coverImageUrl"
            className="form-control"
            value={formData.coverImageUrl}
            onChange={handleInputChange}
            disabled={loading}
            placeholder="https://example.com/image.jpg"
          />
        </div>

        <div className="form-group">
          <label htmlFor="notes" className="form-label">Notes</label>
          <textarea
            id="notes"
            name="notes"
            className="form-control"
            value={formData.notes}
            onChange={handleInputChange}
            disabled={loading}
            rows={3}
            placeholder="Any additional notes about this comic..."
          />
        </div>

        <div className="d-flex gap-2 justify-content-end">
          <Button
            type="button"
            variant="outline"
            onClick={onCancel}
            disabled={loading}
          >
            Cancel
          </Button>
          <Button
            type="submit"
            variant="primary"
            loading={loading}
          >
            {comic ? 'Update Comic' : 'Add Comic'}
          </Button>
        </div>
      </form>
    </div>
  );
};

export default ComicForm;