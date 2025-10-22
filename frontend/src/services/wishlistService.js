import apiClient from './api';

export const wishlistService = {
  getWishlist: async (page = 1, pageSize = 10, filters) => {
    let endpoint = `/wishlist?page=${page}&pageSize=${pageSize}`;
    
    if (filters?.publisher) {
      endpoint += `&publisher=${encodeURIComponent(filters.publisher)}`;
    }
    
    if (filters?.priority) {
      endpoint += `&priority=${filters.priority}`;
    }
    
    if (filters?.desiredCondition) {
      endpoint += `&desiredCondition=${filters.desiredCondition}`;
    }
    
    if (filters?.searchTerm) {
      endpoint += `&search=${encodeURIComponent(filters.searchTerm)}`;
    }

    if (filters?.sortBy) {
      endpoint += `&sortBy=${filters.sortBy}`;
    }

    if (filters?.sortOrder) {
      endpoint += `&sortOrder=${filters.sortOrder}`;
    }
    
    return await apiClient.get(endpoint);
  },

  getWishlistItem: async (id) => {
    return await apiClient.get(`/wishlist/${id}`);
  },

  createWishlistItem: async (wishlistData) => {
    return await apiClient.post('/wishlist', wishlistData);
  },

  updateWishlistItem: async (id, wishlistData) => {
    return await apiClient.put(`/wishlist/${id}`, wishlistData);
  },

  deleteWishlistItem: async (id) => {
    return await apiClient.delete(`/wishlist/${id}`);
  },

  convertToOwned: async (id, conversionData) => {
    return await apiClient.post(`/wishlist/${id}/convert`, conversionData);
  }
};
