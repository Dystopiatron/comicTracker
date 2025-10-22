import React, { useState } from 'react';
import { getConditionName, formatCurrency } from '../../utils/helpers';
import Button from '../Common/Button';
import ErrorMessage from '../Common/ErrorMessage';

const ConvertToOwnedModal = ({ item, onConvert, onCancel, loading = false }) => {
  const [formData, setFormData] = useState({
    purchasePrice: item?.targetPrice || '',
    actualCondition: item?.desiredCondition || 6, // Default to NearMint enum value
    purchaseDate: new Date().toISOString().split('T')[0]
  });
  const [errors, setErrors] = useState({});
  const [submitError, setSubmitError] = useState('');

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    let processedValue = value;

    if (name === 'purchasePrice') {
      processedValue = value ? parseFloat(value) : '';
    } else if (name === 'actualCondition') {
      processedValue = parseInt(value);
    }

    setFormData(prev => ({
      ...prev,
      [name]: processedValue
    }));

    // Clear error for this field
    if (errors[name]) {
      setErrors(prev => ({
        ...prev,
        [name]: ''
      }));
    }
  };

  const validateForm = () => {
    const newErrors = {};

    if (!formData.purchasePrice || formData.purchasePrice < 0) {
      newErrors.purchasePrice = 'Purchase price is required and must be positive';
    }

    if (!formData.actualCondition) {
      newErrors.actualCondition = 'Condition is required';
    }

    if (!formData.purchaseDate) {
      newErrors.purchaseDate = 'Purchase date is required';
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
      await onConvert(item.id, formData);
    } catch (error) {
      setSubmitError(error instanceof Error ? error.message : 'Failed to convert wishlist item');
    }
  };

  if (!item) return null;

  return (
    <div className="modal-overlay" onClick={onCancel}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h2>Convert to Owned Comic</h2>
          <button className="modal-close-btn" onClick={onCancel}>&times;</button>
        </div>

        <div className="modal-body">
          {submitError && <ErrorMessage message={submitError} onDismiss={() => setSubmitError('')} />}

          <div className="wishlist-convert-info">
            <h3>{item.seriesName} {item.issueNumber?.startsWith('#') ? item.issueNumber : `#${item.issueNumber || ''}`}</h3>
            <p><strong>Publisher:</strong> {item.publisher}</p>
            <p><strong>Desired Condition:</strong> {getConditionName(item.desiredCondition)}</p>
            {item.targetPrice && (
              <p><strong>Target Price:</strong> {formatCurrency(item.targetPrice)}</p>
            )}
          </div>

          <hr />

          <p className="text-muted mb-3">
            Enter the details of your purchase to add this comic to your collection:
          </p>

          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label htmlFor="purchasePrice" className="form-label">Purchase Price *</label>
              <input
                type="number"
                id="purchasePrice"
                name="purchasePrice"
                className="form-control"
                value={formData.purchasePrice}
                onChange={handleInputChange}
                disabled={loading}
                min="0"
                step="0.01"
                placeholder="0.00"
              />
              {errors.purchasePrice && <div className="form-error">{errors.purchasePrice}</div>}
            </div>

            <div className="form-group">
              <label htmlFor="actualCondition" className="form-label">Actual Condition *</label>
              <select
                id="actualCondition"
                name="actualCondition"
                className="form-control form-select"
                value={formData.actualCondition}
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
              {errors.actualCondition && <div className="form-error">{errors.actualCondition}</div>}
            </div>

            <div className="form-group">
              <label htmlFor="purchaseDate" className="form-label">Purchase Date *</label>
              <input
                type="date"
                id="purchaseDate"
                name="purchaseDate"
                className="form-control"
                value={formData.purchaseDate}
                onChange={handleInputChange}
                disabled={loading}
                max={new Date().toISOString().split('T')[0]}
              />
              {errors.purchaseDate && <div className="form-error">{errors.purchaseDate}</div>}
            </div>

            <div className="d-flex gap-2 justify-content-end mt-4">
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
                variant="success"
                loading={loading}
              >
                Add to Collection
              </Button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ConvertToOwnedModal;
