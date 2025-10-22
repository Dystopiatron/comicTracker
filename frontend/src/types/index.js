// Comic condition constants
export const ComicCondition = {
  Poor: 1,
  Fair: 2,
  Good: 3,
  Fine: 4,
  VeryFine: 5,
  NearMint: 6,
  Mint: 7
};

export const COMIC_CONDITIONS = [
  { value: ComicCondition.Poor, label: 'Poor' },
  { value: ComicCondition.Fair, label: 'Fair' },
  { value: ComicCondition.Good, label: 'Good' },
  { value: ComicCondition.Fine, label: 'Fine' },
  { value: ComicCondition.VeryFine, label: 'Very Fine' },
  { value: ComicCondition.NearMint, label: 'Near Mint' },
  { value: ComicCondition.Mint, label: 'Mint' }
];

export const PUBLISHERS = [
  'Marvel',
  'DC',
  'Image',
  'Dark Horse',
  'Vertigo',
  'Marvel Epic',
  'Mirage Studios',
  'Cartoon Books',
  'Pantheon Books',
  'IDW',
  'Valiant',
  'Other'
];

// Wishlist priority constants
export const WISHLIST_PRIORITIES = [
  { value: 1, label: 'Highest Priority', color: '#dc3545' },
  { value: 2, label: 'High Priority', color: '#fd7e14' },
  { value: 3, label: 'Medium Priority', color: '#ffc107' },
  { value: 4, label: 'Low Priority', color: '#6c757d' }
];

// Wishlist sort options
export const WISHLIST_SORT_OPTIONS = [
  { value: 'seriesname', label: 'Series Name' },
  { value: 'issuenumber', label: 'Issue Number' },
  { value: 'priority', label: 'Priority' },
  { value: 'targetprice', label: 'Target Price' },
  { value: 'publisher', label: 'Publisher' },
  { value: 'dateadded', label: 'Date Added' }
];