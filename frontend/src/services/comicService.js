import apiClient from './api';

export const comicService = {
  getComics: async (page = 1, pageSize = 10, filters) => {
    let endpoint = `/comics?page=${page}&pageSize=${pageSize}`;
    
    if (filters?.publisher) {
      endpoint += `&publisher=${encodeURIComponent(filters.publisher)}`;
    }
    
    if (filters?.condition) {
      endpoint += `&condition=${filters.condition}`;
    }
    
    if (filters?.searchTerm) {
      endpoint += `&search=${encodeURIComponent(filters.searchTerm)}`;
    }
    
    return await apiClient.get(endpoint);
  },

  getComic: async (id) => {
    return await apiClient.get(`/comics/${id}`);
  },

  createComic: async (comicData) => {
    return await apiClient.post('/comics', comicData);
  },

  updateComic: async (id, comicData) => {
    return await apiClient.put(`/comics/${id}`, comicData);
  },

  deleteComic: async (id) => {
    return await apiClient.delete(`/comics/${id}`);
  },

  searchComics: async (searchTerm) => {
    return await apiClient.get(`/comics/search?searchTerm=${encodeURIComponent(searchTerm)}`);
  },

  getStatistics: async () => {
    return await apiClient.get('/statistics/overview');
  },

  getPublisherStats: async () => {
    return await apiClient.get('/statistics/by-publisher');
  },

  getConditionStats: async () => {
    return await apiClient.get('/statistics/by-condition');
  }
};
