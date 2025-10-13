import apiClient from './api';

class AuthService {
  async login(credentials) {
    const response = await apiClient.post('/auth/login', credentials);
    
    if (response.success && response.data) {
      // The API returns nested data structure: response.data.data contains token and user
      const authData = response.data.data || response.data;
      if (authData.token && authData.user) {
        localStorage.setItem('token', authData.token);
        localStorage.setItem('user', JSON.stringify(authData.user));
      }
    }
    
    return response;
  }

  async register(userData) {
    return await apiClient.post('/auth/register', userData);
  }

  logout() {
    try {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
    } catch (error) {
      console.error('Logout error:', error);
    }
  }

  isAuthenticated() {
    return !!localStorage.getItem('token');
  }

  async getCurrentUser() {
    return await apiClient.get('/users/me');
  }

  async getProfile() {
    return await apiClient.get('/users/profile');
  }

  async updateProfile(userData) {
    return await apiClient.put('/users/profile', userData);
  }
}

const authService = new AuthService();
export default authService;
