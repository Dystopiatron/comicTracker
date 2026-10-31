import api from './api';

/**
 * ComicVine API Service
 * Handles all interactions with the ComicVine API through our backend
 */

/**
 * The shared API client returns { success, message, data }, where data is the
 * API's ApiResponse body. Surface that body, or a failure object when the
 * HTTP call itself failed.
 */
const unwrapApiResponse = (response) => {
  if (!response?.success) {
    return {
      success: false,
      message: response?.message || 'Request failed',
    };
  }

  return response.data ?? { success: false, message: 'Empty response from server' };
};

/**
 * Check if ComicVine API is configured and available
 * @returns {Promise<Object>} Status object with isConfigured boolean and message
 */
export const getComicVineStatus = async () => {
  try {
    const body = unwrapApiResponse(await api.get('/comicvine/status'));
    if (!body.success || !body.data) {
      return { isConfigured: false, message: body.message || 'Unable to check ComicVine status' };
    }
    return body.data;
  } catch (error) {
    return { isConfigured: false, message: 'Unable to check ComicVine status' };
  }
};

/**
 * Search ComicVine for comics, characters, volumes, etc.
 * @param {string} query - Search query
 * @param {Object} options - Search options
 * @param {string} [options.resourceType] - Filter by resource type (Issue, Volume, Character, etc.)
 * @param {number} [options.page=1] - Page number
 * @param {number} [options.pageSize=10] - Results per page
 * @returns {Promise<Object>} Search results with pagination
 */
export const searchComicVine = async (query, options = {}) => {
  const { resourceType, page = 1, pageSize = 10 } = options;
  
  const params = new URLSearchParams({
    query,
    page: page.toString(),
    pageSize: pageSize.toString(),
  });
  
  if (resourceType) {
    params.append('resourceType', resourceType);
  }
  
  return unwrapApiResponse(await api.get(`/comicvine/search?${params.toString()}`));
};

/**
 * Search specifically for comic issues
 * @param {string} query - Search query
 * @param {number} [page=1] - Page number
 * @param {number} [pageSize=10] - Results per page
 */
export const searchIssues = async (query, page = 1, pageSize = 10) => {
  const params = new URLSearchParams({
    query,
    page: page.toString(),
    pageSize: pageSize.toString()
  });
  return unwrapApiResponse(await api.get(`/comicvine/search/issues?${params.toString()}`));
};

/**
 * Search specifically for volumes (comic series)
 * @param {string} query - Search query
 * @param {number} [page=1] - Page number
 * @param {number} [pageSize=10] - Results per page
 */
export const searchVolumes = async (query, page = 1, pageSize = 10) => {
  const params = new URLSearchParams({
    query,
    page: page.toString(),
    pageSize: pageSize.toString()
  });
  return unwrapApiResponse(await api.get(`/comicvine/search/volumes?${params.toString()}`));
};

/**
 * Get detailed information about a specific issue
 * @param {number} issueId - ComicVine issue ID
 */
export const getIssue = async (issueId) => {
  return unwrapApiResponse(await api.get(`/comicvine/issue/${issueId}`));
};

/**
 * Get detailed information about a volume (comic series)
 * @param {number} volumeId - ComicVine volume ID
 */
export const getVolume = async (volumeId) => {
  return unwrapApiResponse(await api.get(`/comicvine/volume/${volumeId}`));
};

/**
 * Get all issues for a specific volume
 * @param {number} volumeId - ComicVine volume ID
 */
export const getVolumeIssues = async (volumeId) => {
  return unwrapApiResponse(await api.get(`/comicvine/volume/${volumeId}/issues`));
};

/**
 * Get detailed information about a character
 * @param {number} characterId - ComicVine character ID
 */
export const getCharacter = async (characterId) => {
  return unwrapApiResponse(await api.get(`/comicvine/character/${characterId}`));
};

/**
 * Import a comic from ComicVine into user's collection
 * @param {Object} importData - Import request data
 * @param {number} importData.comicVineIssueId - ComicVine issue ID to import
 * @param {string} importData.condition - Comic condition (Mint, NearMint, VeryFine, etc.)
 * @param {number} [importData.purchasePrice] - Optional purchase price
 * @param {string} [importData.notes] - Optional notes
 */
export const importToCollection = async (importData) => {
  return unwrapApiResponse(await api.post('/comicvine/import', importData));
};

/**
 * Resource types available for filtering search results
 * Note: Publisher and StoryArc removed due to ComicVine API limitations
 */
export const ResourceTypes = {
  ISSUE: 'Issue',
  VOLUME: 'Volume',
  CHARACTER: 'Character',
  PERSON: 'Person',
  TEAM: 'Team'
};

/**
 * Map ComicVine issue to local comic format for adding to collection
 * @param {Object} issue - ComicVine issue object
 * @param {Object} additionalData - Additional data (condition, price, notes)
 * @returns {Object} Formatted comic data for local collection
 */
export const mapComicVineIssueToLocal = (issue, additionalData = {}) => {
  return {
    seriesName: issue.volume?.name || 'Unknown Series',
    issueNumber: issue.issueNumber || '#1',
    publisher: issue.volume?.publisher?.name || null,
    coverImageUrl: issue.coverImageUrl || issue.smallImageUrl || null,
    notes: additionalData.notes || null,
    condition: additionalData.condition || 'VeryFine',
    purchasePrice: additionalData.purchasePrice || null,
    comicVineId: issue.id
  };
};

const comicVineService = {
  getComicVineStatus,
  searchComicVine,
  searchIssues,
  searchVolumes,
  getIssue,
  getVolume,
  getVolumeIssues,
  getCharacter,
  importToCollection,
  mapComicVineIssueToLocal,
  ResourceTypes
};

export default comicVineService;
