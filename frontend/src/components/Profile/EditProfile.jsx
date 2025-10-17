import React, { useState } from 'react';
import authService from '../../services/authService';
import Button from '../Common/Button';
import ErrorMessage from '../Common/ErrorMessage';

const EditProfile = ({ user, onSave, onCancel }) => {
  const [formData, setFormData] = useState({
    firstName: user.firstName,
    lastName: user.lastName,
    avatarUrl: user.avatarUrl || ''
  });
  const [errors, setErrors] = useState({});
  const [loading, setLoading] = useState(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
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

    if (!formData.firstName.trim()) {
      newErrors.firstName = 'First name is required';
    }

    if (!formData.lastName.trim()) {
      newErrors.lastName = 'Last name is required';
    }

    if (formData.avatarUrl && formData.avatarUrl.trim()) {
      // Basic URL validation - check if it starts with http/https
      const urlPattern = /^https?:\/\/.+/;
      if (!urlPattern.test(formData.avatarUrl.trim())) {
        newErrors.avatarUrl = 'Please enter a valid URL starting with http:// or https://';
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    setLoading(true);

    try {
      // Clean up the data before sending
      const cleanedData = {
        firstName: formData.firstName.trim(),
        lastName: formData.lastName.trim(),
        avatarUrl: formData.avatarUrl.trim() || null
      };
      
      const response = await authService.updateProfile(cleanedData);
      
      if (response.success && response.data) {
        onSave(response.data);
      } else {
        setErrors({ general: response.message });
      }
    } catch (err) {
      setErrors({ general: 'Failed to update profile. Please try again.' });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="card">
      <div className="card-header">
        <h3>Edit Profile</h3>
      </div>

      {errors.general && <ErrorMessage message={errors.general} onDismiss={() => setErrors(prev => ({ ...prev, general: '' }))} />}

      <form onSubmit={handleSubmit}>
        <div className="form-row">
          <div className="form-group form-group-flex">
            <label htmlFor="firstName" className="form-label">First Name</label>
            <input
              type="text"
              id="firstName"
              name="firstName"
              className="form-control"
              value={formData.firstName}
              onChange={handleInputChange}
              disabled={loading}
            />
            {errors.firstName && <div className="form-error">{errors.firstName}</div>}
          </div>

          <div className="form-group form-group-flex">
            <label htmlFor="lastName" className="form-label">Last Name</label>
            <input
              type="text"
              id="lastName"
              name="lastName"
              className="form-control"
              value={formData.lastName}
              onChange={handleInputChange}
              disabled={loading}
            />
            {errors.lastName && <div className="form-error">{errors.lastName}</div>}
          </div>
        </div>

        <div className="form-group">
          <label htmlFor="avatarUrl" className="form-label">Avatar URL (Optional)</label>
          <input
            type="url"
            id="avatarUrl"
            name="avatarUrl"
            className="form-control"
            value={formData.avatarUrl}
            onChange={handleInputChange}
            disabled={loading}
            placeholder="https://example.com/avatar.jpg"
          />
          {errors.avatarUrl && <div className="form-error">{errors.avatarUrl}</div>}
        </div>

        <div className="alert alert-info">
          <small>
            <strong>Note:</strong> Username and email cannot be changed through this form for security reasons.
          </small>
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
            Save Changes
          </Button>
        </div>
      </form>
    </div>
  );
};

export default EditProfile;