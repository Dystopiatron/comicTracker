import React, { useState, useEffect } from 'react';
import { WISHLIST_PRIORITIES, PUBLISHERS } from '../../types';
import Button from '../Common/Button';
import ErrorMessage from '../Common/ErrorMessage';

const WishlistForm = ({ item, onSave, onCancel, loading = false }) => {
  const [formData, setFormData] = useState({
    seriesName: '',
    issueNumber: '',
    publisher: '',
    desiredCondition: 6, // Default to NearMint (enum value)
    targetPrice: undefined,
    priority: 3, // Default to Medium
    notes: ''
  });
  const [errors, setErrors] = useState({});
  const [submitError, setSubmitError] = useState('');

  // Initialize form data when item prop changes
  useEffect(() => {
    if (item) {
      // Convert condition string to enum value if needed
      let conditionValue = item.desiredCondition;
      if (typeof item.desiredCondition === 'string') {
        const conditionMap = {
          'Poor': 1,
          'Fair': 2,
          'Good': 3,
          'Fine': 4,
          'VeryFine': 5,
          'NearMint': 6,
          'Mint': 7
        };
        conditionValue = conditionMap[item.desiredCondition] || 6;
      }

      setFormData({
        seriesName: item.seriesName || '',
        issueNumber: item.issueNumber || '',
        publisher: item.publisher || '',
        desiredCondition: conditionValue,
        targetPrice: item.targetPrice,
        priority: item.priority || 3,
        notes: item.notes || ''
      });
    }
  }, [item]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    let processedValue = value;

    // Handle numeric fields
    if (name === 'priority' || name === 'desiredCondition') {
      processedValue = parseInt(value);
    } else if (name === 'targetPrice') {
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

    if (formData.targetPrice !== undefined && formData.targetPrice < 0) {
      newErrors.targetPrice = 'Price cannot be negative';
    }

    if (formData.priority < 1 || formData.priority > 4) {
      newErrors.priority = 'Priority must be between 1 and 4';
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
        notes: formData.notes.trim() || null
      };
      
      await onSave(cleanedData);
    } catch (error) {
      setSubmitError(error instanceof Error ? error.message : 'Failed to save wishlist item');
    }
  };

  return (
    <div className="card">
      <div className="card-header">
        <h2>{item ? 'Edit Wishlist Item' : 'Add to Wishlist'}</h2>
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
            <label htmlFor="desiredCondition" className="form-label">Desired Condition</label>
            <select
              id="desiredCondition"
              name="desiredCondition"
              className="form-control form-select"
              value={formData.desiredCondition}
              onChange={handleInputChange}
              disabled={loading}
            >
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

        <div className="form-row">
          <div className="form-group form-group-flex">
            <label htmlFor="priority" className="form-label">Priority *</label>
            <select
              id="priority"
              name="priority"
              className="form-control form-select"
              value={formData.priority}
              onChange={handleInputChange}
              disabled={loading}
            >
              {WISHLIST_PRIORITIES.map(priority => (
                <option key={priority.value} value={priority.value}>
                  {priority.label}
                </option>
              ))}
            </select>
            {errors.priority && <div className="form-error">{errors.priority}</div>}
          </div>

          <div className="form-group form-group-flex">
            <label htmlFor="targetPrice" className="form-label">Target Price (Max)</label>
            <input
              type="number"
              id="targetPrice"
              name="targetPrice"
              className="form-control"
              value={formData.targetPrice || ''}
              onChange={handleInputChange}
              disabled={loading}
              min="0"
              step="0.01"
              placeholder="0.00"
            />
            {errors.targetPrice && <div className="form-error">{errors.targetPrice}</div>}
          </div>
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
            placeholder="Why do you want this comic? Any specific details..."
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
            {item ? 'Update Item' : 'Add to Wishlist'}
          </Button>
        </div>
      </form>
    </div>
  );
};

export default WishlistForm;
