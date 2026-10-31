# ComicTracker Frontend

React 19 interface for Comic Tracker. The API client uses `fetch` and talks to `http://localhost:8000/api`.

## Setup

```bash
npm install
npm start
```

The dev server runs at http://localhost:3000.

## Scripts

| Command | Description |
|---------|-------------|
| `npm start` | Development server |
| `npm run build` | Production build (includes lint) |
| `npm test` | Test runner |

## ComicVine search

The Add Comic page has a ComicVine search tab and the existing manual form. Search calls the backend `/api/comicvine` endpoints. If the backend has no API key, the search tab explains that and manual entry still works.

Imported issues use the same collection as comics added by hand or converted from the wishlist.
