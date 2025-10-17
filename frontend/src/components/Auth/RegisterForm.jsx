import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import Button from '../Common/Button';
import ErrorMessage from '../Common/ErrorMessage';
import { validateEmail, validatePassword } from '../../utils/helpers';

const RegisterForm = () => {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: ''
  });
  const [errors, setErrors] = useState({});
  const [loading, setLoading] = useState(false);
  
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleInputChange = ( e) => {
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

    if (!formData.username.trim()) {
      newErrors.username = 'Username is required';
    }

    if (!formData.email.trim()) {
      newErrors.email = 'Email is required';
    } else if (!validateEmail(formData.email)) {
      newErrors.email = 'Please enter a valid email address';
    }

    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else {
      const passwordValidation = validatePassword(formData.password);
      if (!passwordValidation.isValid) {
        newErrors.password = passwordValidation.message;
      }
    }

    if (!formData.confirmPassword) {
      newErrors.confirmPassword = 'Please confirm your password';
    } else if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }

    if (!formData.firstName.trim()) {
      newErrors.firstName = 'First name is required';
    }

    if (!formData.lastName.trim()) {
      newErrors.lastName = 'Last name is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async ( e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    setLoading(true);

    try {
      const { confirmPassword, ...registerData } = formData;
      const result = await register(registerData);
      
      if (result.success) {
        navigate('/');
      } else {
        setErrors({ general: result.message });
      }
    } catch (err) {
      setErrors({ general: 'Registration failed. Please try again.' });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container auth-container">
      <div className="card">
        <div className="card-header text-center">
          <h2>Create Account</h2>
          <p className="text-muted">Join Comic Tracker to manage your collection</p>
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
            <label htmlFor="username" className="form-label">Username</label>
            <input
              type="text"
              id="username"
              name="username"
              className="form-control"
              value={formData.username}
              onChange={handleInputChange}
              disabled={loading}
            />
            {errors.username && <div className="form-error">{errors.username}</div>}
          </div>

          <div className="form-group">
            <label htmlFor="email" className="form-label">Email</label>
            <input
              type="email"
              id="email"
              name="email"
              className="form-control"
              value={formData.email}
              onChange={handleInputChange}
              disabled={loading}
            />
            {errors.email && <div className="form-error">{errors.email}</div>}
          </div>

          <div className="form-group">
            <label htmlFor="password" className="form-label">Password</label>
            <input
              type="password"
              id="password"
              name="password"
              className="form-control"
              value={formData.password}
              onChange={handleInputChange}
              disabled={loading}
            />
            {errors.password && <div className="form-error">{errors.password}</div>}
          </div>

          <div className="form-group">
            <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
            <input
              type="password"
              id="confirmPassword"
              name="confirmPassword"
              className="form-control"
              value={formData.confirmPassword}
              onChange={handleInputChange}
              disabled={loading}
            />
            {errors.confirmPassword && <div className="form-error">{errors.confirmPassword}</div>}
          </div>

          <Button
            type="submit"
            variant="primary"
            size="lg"
            loading={loading}
            className="full-width-btn"
          >
            Create Account
          </Button>
        </form>

        <div className="text-center mt-3">
          <p>
            Already have an account?{' '}
            <Link to="/login" className="text-primary">
              Login here
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default RegisterForm;