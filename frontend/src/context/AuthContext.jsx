import React, { createContext, useContext, useState, useEffect } from 'react';
import authService from '../services/authService';

const AuthContext = createContext({});

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem('token'));
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const initAuth = async () => {
      const savedToken = localStorage.getItem('token');
      const savedUser = localStorage.getItem('user');
      
      if (savedToken && savedUser) {
        setToken(savedToken);
        try {
          setUser(JSON.parse(savedUser));
        } catch {
          // If parsing fails, clear the saved data
          localStorage.removeItem('user');
          localStorage.removeItem('token');
        }
      }
      setLoading(false);
    };

    initAuth();
  }, []);

  const login = async (credentials) => {
    const response = await authService.login(credentials);
    
    console.log('Login response:', response);
    console.log('Response data:', response.data);
    
    if (response.success && response.data) {
      // API returns nested structure: response.data.data contains token and user
      const authData = response.data.data || response.data;
      if (authData.token && authData.user) {
        setUser(authData.user);
        setToken(authData.token);
        localStorage.setItem('token', authData.token);
        localStorage.setItem('user', JSON.stringify(authData.user));
      } else {
        console.error('Missing token or user in response data:', authData);
        return {
          success: false,
          message: 'Invalid response format from server'
        };
      }
    }
    
    return response;
  };

  const register = async (userData) => {
    const response = await authService.register(userData);
    
    if (response.success && response.data) {
      // API returns nested structure: response.data.data contains token and user
      const authData = response.data.data || response.data;
      if (authData.token && authData.user) {
        setUser(authData.user);
        setToken(authData.token);
        localStorage.setItem('token', authData.token);
        localStorage.setItem('user', JSON.stringify(authData.user));
      }
    }
    
    return response;
  };

  const logout = () => {
    console.log('Logout called - clearing auth state');
    authService.logout();
    setUser(null);
    setToken(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('refreshToken');
    console.log('Logout completed - user and token cleared');
  };

  const updateUser = (updatedUser) => {
    setUser(updatedUser);
    localStorage.setItem('user', JSON.stringify(updatedUser));
  };

  const isAuthenticated = !!token && !!user;

  const value = {
    user,
    token,
    loading,
    isAuthenticated,
    login,
    register,
    logout,
    updateUser
  };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

export default AuthContext;
