const API_BASE_URL = 'http://localhost:8000/api';

class ApiClient {
  constructor() {
    this.onUnauthorized = null; // Callback for 401 errors
  }

  setUnauthorizedCallback(callback) {
    this.onUnauthorized = callback;
  }

  handleUnauthorized() {
    if (this.onUnauthorized) {
      this.onUnauthorized();
    }
  }

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

      // Handle 401 Unauthorized
      if (response.status === 401) {
        this.handleUnauthorized();
        return {
          success: false,
          message: 'Your session has expired. Please login again.',
          unauthorized: true,
        };
      }

      // Handle empty response body (e.g., 204 No Content)
      const contentType = response.headers.get('content-type');
      let data = null;
      if (contentType && contentType.includes('application/json')) {
        try {
          data = await response.json();
        } catch (e) {
          // Response claimed to be JSON but wasn't - handle gracefully
          data = null;
        }
      }
      
      if (!response.ok) {
        return {
          success: false,
          message: data?.message || 'Request failed',
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

      // Handle 401 Unauthorized
      if (response.status === 401) {
        this.handleUnauthorized();
        return {
          success: false,
          message: 'Your session has expired. Please login again.',
          unauthorized: true,
        };
      }

      // Handle empty response body
      const contentType = response.headers.get('content-type');
      let data = null;
      if (contentType && contentType.includes('application/json')) {
        try {
          data = await response.json();
        } catch (e) {
          // Response claimed to be JSON but wasn't - handle gracefully
          data = null;
        }
      }
      
      if (!response.ok) {
        return {
          success: false,
          message: data?.message || 'Request failed',
        };
      }

      return {
        success: true,
        message: data?.message || 'Success',
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

      // Handle 401 Unauthorized
      if (response.status === 401) {
        this.handleUnauthorized();
        return {
          success: false,
          message: 'Your session has expired. Please login again.',
          unauthorized: true,
        };
      }

      // Handle empty response body
      const contentType = response.headers.get('content-type');
      let data = null;
      if (contentType && contentType.includes('application/json')) {
        try {
          data = await response.json();
        } catch (e) {
          // Response claimed to be JSON but wasn't - handle gracefully
          data = null;
        }
      }
      
      if (!response.ok) {
        return {
          success: false,
          message: data?.message || 'Request failed',
        };
      }

      return {
        success: true,
        message: data?.message || 'Success',
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

      // Handle 401 Unauthorized
      if (response.status === 401) {
        this.handleUnauthorized();
        return {
          success: false,
          message: 'Your session has expired. Please login again.',
          unauthorized: true,
        };
      }

      // Handle empty response body
      const contentType = response.headers.get('content-type');
      let data = null;
      if (contentType && contentType.includes('application/json')) {
        try {
          data = await response.json();
        } catch (e) {
          // Response claimed to be JSON but wasn't - handle gracefully
          data = null;
        }
      }

      if (!response.ok) {
        return {
          success: false,
          message: data?.message || 'Request failed',
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
}

export const apiClient = new ApiClient();
export default apiClient;