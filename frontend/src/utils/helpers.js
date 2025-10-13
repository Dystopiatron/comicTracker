import { COMIC_CONDITIONS } from '../types';

export const getConditionName = (condition) => {
  // Handle both string and numeric conditions
  if (typeof condition === 'string') {
    // Map string conditions to display names
    const conditionMap = {
      'Poor': 'Poor',
      'Fair': 'Fair', 
      'Good': 'Good',
      'Fine': 'Fine',
      'VeryFine': 'Very Fine',
      'NearMint': 'Near Mint',
      'Mint': 'Mint'
    };
    return conditionMap[condition] || condition;
  }
  
  // Handle numeric conditions (legacy)
  const conditionObj = COMIC_CONDITIONS.find(c => c.value === condition);
  return conditionObj ? conditionObj.label : 'Unknown';
};

export const formatCurrency = (amount) => {
  if (amount === undefined || amount === null) return 'N/A';
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  }).format(amount);
};

export const formatDate = (dateString) => {
  const date = new Date(dateString);
  return date.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
  });
};

export const truncateText = (text, maxLength) => {
  if (text.length <= maxLength) return text;
  return text.substring(0, maxLength) + '...';
};

export const validateEmail = (email) => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
};

export const validatePassword = (password) => {
  if (password.length < 6) {
    return { isValid: false, message: 'Password must be at least 6 characters long' };
  }
  
  if (!/(?=.*[a-z])/.test(password)) {
    return { isValid: false, message: 'Password must contain at least one lowercase letter' };
  }
  
  if (!/(?=.*[A-Z])/.test(password)) {
    return { isValid: false, message: 'Password must contain at least one uppercase letter' };
  }
  
  if (!/(?=.*\d)/.test(password)) {
    return { isValid: false, message: 'Password must contain at least one number' };
  }
  
  return { isValid: true, message: '' };
};

export const debounce = (func, delay) => {
  let timeoutId;
  
  return (...args) => {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => func(...args), delay);
  };
};
