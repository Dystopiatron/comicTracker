import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import Button from '../Common/Button';
import ErrorMessage from '../Common/ErrorMessage';

const LoginForm = () => {
  const [credentials, setCredentials] = useState({
    username: '',
    password: ''
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  
  const { login } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  
  const from = location.state?.from?.pathname || '/';
  const demoUsername = process.env.REACT_APP_DEMO_USERNAME;
  const demoPassword = process.env.REACT_APP_DEMO_PASSWORD;
  const hasDemoLogin = Boolean(demoUsername && demoPassword);

  const handleInputChange = ( e) => {
    const { name, value } = e.target;
    setCredentials(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async ( e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const result = await login(credentials);
      if (result.success) {
        navigate(from, { replace: true });
      } else {
        setError(result.message);
      }
    } catch (err) {
      setError('Login failed. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  const handleDemoLogin = () => {
    if (!hasDemoLogin) {
      return;
    }

    setCredentials({
      username: demoUsername,
      password: demoPassword
    });
  };

  return (
    <div className="container auth-container-sm">
      <div className="card">
        <div className="card-header text-center">
          <h2>Login to Comic Tracker</h2>
          <p className="text-muted">Access your comic collection</p>
        </div>

        {hasDemoLogin && (
          <div className="text-center mb-3">
            <button
              type="button"
              onClick={handleDemoLogin}
              className="btn btn-outline btn-sm"
            >
              Use demo account
            </button>
          </div>
        )}

        {error && <ErrorMessage message={error} onDismiss={() => setError('')} />}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="username" className="form-label">Username</label>
            <input
              type="text"
              id="username"
              name="username"
              className="form-control"
              value={credentials.username}
              onChange={handleInputChange}
              required
              disabled={loading}
            />
          </div>

          <div className="form-group">
            <label htmlFor="password" className="form-label">Password</label>
            <input
              type="password"
              id="password"
              name="password"
              className="form-control"
              value={credentials.password}
              onChange={handleInputChange}
              required
              disabled={loading}
            />
          </div>

          <Button
            type="submit"
            variant="primary"
            size="lg"
            loading={loading}
            className="full-width-btn"
          >
            Login
          </Button>
        </form>

        <div className="text-center mt-3">
          <p>
            Don't have an account?{' '}
            <Link to="/register" className="text-primary">
              Register here
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default LoginForm;