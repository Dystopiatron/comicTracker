import api from './api';

const adminService = {
  // Get all users (admin only)
  getAllUsers: async () => {
    try {
      const response = await api.get('/admin/users');
      console.log('getAllUsers response:', response); // Debug log
      return response;
    } catch (error) {
      console.error('Error fetching all users:', error);
      return {
        success: false,
        message: error.message || 'Failed to fetch users'
      };
    }
  },

  // Get user with all their comics (admin only)
  getUserWithComics: async (userId) => {
    try {
      const response = await api.get(`/admin/users/${userId}`);
      console.log('getUserWithComics response:', response); // Debug log
      return response;
    } catch (error) {
      console.error('Error fetching user with comics:', error);
      return {
        success: false,
        message: error.message || 'Failed to fetch user details'
      };
    }
  },

  // Update any user (admin only)
  updateUser: async (userId, userData) => {
    try {
      const response = await api.put(`/admin/users/${userId}`, userData);
      console.log('updateUser response:', response); // Debug log
      return response;
    } catch (error) {
      console.error('Error updating user:', error);
      return {
        success: false,
        message: error.message || 'Failed to update user'
      };
    }
  },

  // Delete a user (admin only)
  deleteUser: async (userId) => {
    try {
      const response = await api.delete(`/admin/users/${userId}`);
      console.log('deleteUser response:', response); // Debug log
      return response;
    } catch (error) {
      console.error('Error deleting user:', error);
      return {
        success: false,
        message: error.message || 'Failed to delete user'
      };
    }
  },

  // Get system statistics (admin only)
  getSystemStatistics: async () => {
    try {
      const response = await api.get('/admin/statistics');
      console.log('getSystemStatistics response:', response); // Debug log
      return response;
    } catch (error) {
      console.error('Error fetching system statistics:', error);
      return {
        success: false,
        message: error.message || 'Failed to fetch statistics'
      };
    }
  }
};

export default adminService;