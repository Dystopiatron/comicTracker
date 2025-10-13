const API_BASE_URL = 'http://localhost:8000/api';

class ApiClient {
  getAuthHeaders() {
    const token = localStorage.getItem('token');
    const headers = {
      'Content-Type': 'application/json',
    };
    
    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }
    
    return headers;
  }

  async get(endpoint) {
    try {
      const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'GET',
        headers: this.getAuthHeaders(),
      });

      const data = await response.json();
      
      if (!response.ok) {
        return {
          success: false,
          message: data.message || 'Request failed',
        };
      }

      return {
        success: true,
        message: 'Success',
        data,
      };
    } catch (error) {
      return {
        success: false,
        message: error instanceof Error ? error.message : 'Network error',
      };
    }
  }

  async post(endpoint, body) {
    try {
      // For login/register endpoints, don't send auth headers
      const isAuthEndpoint = endpoint === '/auth/login' || endpoint === '/auth/register';
      const headers = isAuthEndpoint ? 
        { 'Content-Type': 'application/json' } : 
        this.getAuthHeaders();

      const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(body),
      });

      const data = await response.json();
      
      if (!response.ok) {
        return {
          success: false,
          message: data.message || 'Request failed',
        };
      }

      return {
        success: true,
        message: data.message || 'Success',
        data,
      };
    } catch (error) {
      return {
        success: false,
        message: error instanceof Error ? error.message : 'Network error',
      };
    }
  }

  async put(endpoint, body) {
    try {
      const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'PUT',
        headers: this.getAuthHeaders(),
        body: JSON.stringify(body),
      });

      const data = await response.json();
      
      if (!response.ok) {
        return {
          success: false,
          message: data.message || 'Request failed',
        };
      }

      return {
        success: true,
        message: data.message || 'Success',
        data,
      };
    } catch (error) {
      return {
        success: false,
        message: error instanceof Error ? error.message : 'Network error',
      };
    }
  }

  async delete(endpoint) {
    try {
      const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        method: 'DELETE',
        headers: this.getAuthHeaders(),
      });

      if (!response.ok) {
        const data = await response.json();
        return {
          success: false,
          message: data.message || 'Request failed',
        };
      }

      // Handle cases where DELETE returns no content
      let data;
      try {
        data = await response.json();
      } catch {
        data = null;
      }

      return {
        success: true,
        message: 'Success',
        data,
      };
    } catch (error) {
      return {
        success: false,
        message: error instanceof Error ? error.message : 'Network error',
      };
    }
  }
}

export const apiClient = new ApiClient();
export default apiClient;