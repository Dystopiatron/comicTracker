# CSS Refactor TODO

*Hardcoded values to fix when I come back to this project*

---

## Overview

Found **122 hardcoded hex colors** across the CSS files. Many of these should be converted to use CSS variables from App.css for consistency and easier theming.

---

## Priority: HIGH 🔴

These files have the most hardcoded values and will have the biggest impact to fix.

### 1. ComicVineSearch.css (51 hardcoded colors!)

**Location:** `frontend/src/components/Comics/ComicVineSearch.css`

This is the worst offender. Almost nothing uses CSS variables.

| Line | Current | Should Be |
|------|---------|-----------|
| 18 | `color: #666` | `var(--gray-500)` |
| 36, 49 | `border: 2px solid #e0e0e0` | `var(--border-color)` |
| 69 | `background-color: #1557b0` | `var(--primary-dark)` |
| 79 | `background-color: #fce8e6` | Create `--error-bg` variable |
| 80 | `color: #c5221f` | `var(--danger-color)` |
| 88 | `background-color: #e8f4fd` | Create `--info-bg` variable |
| 89 | `border: 1px solid #b8daff` | Create `--info-border` variable |
| 103, 108, 115 | `color: #0c5460` | Create `--info-text` variable |
| 125 | `color: #1a73e8` | `var(--primary-color)` |
| 135, 141, 274, 307 | `color: #666` | `var(--gray-500)` |
| 189, 447 | `background-color: #f5f5f5` | `var(--gray-100)` |
| 202 | `color: #999` | `var(--gray-400)` |
| 213, 333, 378, 408 | `color: #333` | `var(--gray-800)` |
| 219, 267 | `background-color: #e8f0fe` | Create `--primary-bg-light` |
| 243, 452 | `background-color: #34a853` | `var(--success-color)` |
| 253, 462 | `background-color: #2d8e47` | Create `--success-dark` |
| 338, 396, 434 | `border: 1px solid #e0e0e0` | `var(--border-color)` |

### 2. Alert.css (24 hardcoded colors)

**Location:** `frontend/src/styles/components/Alert.css`

All the alert state colors are hardcoded. Should create semantic variables.

**Suggested new variables for App.css:**
```css
/* Alert Colors */
--alert-success-bg: #d4edda;
--alert-success-text: #155724;
--alert-success-border: #c3e6cb;

--alert-error-bg: #f8d7da;
--alert-error-text: #721c24;
--alert-error-border: #f5c6cb;

--alert-warning-bg: #fff3cd;
--alert-warning-text: #856404;
--alert-warning-border: #ffeaa7;

--alert-info-bg: #d1ecf1;
--alert-info-text: #0c5460;
--alert-info-border: #bee5eb;
```

### 3. Header.css (3 hardcoded colors)

**Location:** `frontend/src/styles/components/Header.css`

| Line | Current | Should Be |
|------|---------|-----------|
| 3 | `background-color: #e19b04ea` | Create `--navbar-color` |
| 4 | `border-bottom: 1px solid #e7a109` | Create `--navbar-border` |
| 21 | `color: #1f2937` | `var(--text-color)` |

---

## Priority: MEDIUM 🟡

### 4. AddComicPage.css (8 hardcoded colors)

**Location:** `frontend/src/styles/pages/AddComicPage.css`

| Line | Current | Should Be |
|------|---------|-----------|
| 7 | `border-bottom: 2px solid #e0e0e0` | `var(--border-color)` |
| 16 | `color: #666` | `var(--gray-500)` |
| 53 | `border: 1px solid #e0e0e0` | `var(--border-color)` |
| 60 | `background-color: #e8f0fe` | `var(--primary-bg-light)` |

### 5. cards.css (6 hardcoded colors)

**Location:** `frontend/src/styles/components/cards.css`

| Line | Current | Should Be |
|------|---------|-----------|
| 268 | `background-color: #fee2e2` | `var(--danger-color)` with opacity |
| 269 | `color: #991b1b` | Create `--danger-dark` |
| 273 | `background-color: #fef3c7` | `var(--warning-color)` with opacity |
| 274 | `color: #92400e` | Create `--warning-dark` |
| 278 | `background-color: #dbeafe` | `var(--info-color)` with opacity |
| 279 | `color: #1e40af` | Create `--info-dark` |

### 6. Wishlist.css (2 hardcoded colors)

**Location:** `frontend/src/styles/components/Wishlist.css`

| Line | Current | Should Be |
|------|---------|-----------|
| 20 | `background-color: #f8f9fa` | `var(--gray-50)` |
| 28 | `color: #2c3e50` | `var(--gray-800)` |

### 7. Modal.css (2 hardcoded colors)

**Location:** `frontend/src/styles/components/Modal.css`

| Line | Current | Should Be |
|------|---------|-----------|
| 141, 149 | `color: #000` | `var(--black)` |

---

## Priority: LOW 🟢

### 8. App.css (3 hardcoded outside :root)

**Location:** `frontend/src/styles/App.css`

These are button hover states that should reference variables:

| Line | Current | Should Be |
|------|---------|-----------|
| 181 | `background-color: #475569` | `var(--secondary-dark)` (create it) |
| 190 | `background-color: #059669` | Create `--success-dark` |
| 199 | `background-color: #dc2626` | Create `--danger-dark` |

### 9. HomePage.css (1 hardcoded color)

**Location:** `frontend/src/styles/pages/HomePage.css`

| Line | Current | Should Be |
|------|---------|-----------|
| 30 | `#3b82f6` in gradient | `var(--primary-light)` |

---

## New Variables to Add to App.css

When you fix these, add these variables to the `:root` section:

```css
/* Navbar */
--navbar-color: #e19b04ea;
--navbar-border: #e7a109;

/* Dark variants */
--primary-dark: #1d4ed8;  /* already exists */
--success-dark: #059669;
--danger-dark: #dc2626;
--warning-dark: #92400e;
--info-dark: #1e40af;
--secondary-dark: #475569;

/* Light backgrounds */
--primary-bg-light: #e8f0fe;
--success-bg-light: #dcfce7;
--danger-bg-light: #fef2f2;
--warning-bg-light: #fef3c7;
--info-bg-light: #dbeafe;

/* Alert-specific (if you want separate) */
--alert-success-bg: #d4edda;
--alert-success-text: #155724;
--alert-success-border: #c3e6cb;
/* ... etc for error, warning, info */
```

---

## Quick Wins (Do These First)

1. **Header.css** - Only 3 values, quick fix
2. **Modal.css** - Only 2 values
3. **Wishlist.css** - Only 2 values
4. **HomePage.css** - Only 1 value

These 4 files would take like 10 minutes total.

---

## The Big Job

**ComicVineSearch.css** is gonna take a while. Consider:
- Moving it to `/styles/components/` folder for consistency
- Or converting the whole thing to use CSS modules
- At minimum, replace all the `#666` and `#e0e0e0` instances (there's like 20)

---

## Summary

| File | Hardcoded Count | Priority |
|------|-----------------|----------|
| ComicVineSearch.css | 51 | 🔴 HIGH |
| Alert.css | 24 | 🔴 HIGH |
| AddComicPage.css | 8 | 🟡 MEDIUM |
| cards.css | 6 | 🟡 MEDIUM |
| App.css (outside :root) | 3 | 🟢 LOW |
| Header.css | 3 | 🟢 LOW (quick win) |
| Wishlist.css | 2 | 🟢 LOW (quick win) |
| Modal.css | 2 | 🟢 LOW (quick win) |
| HomePage.css | 1 | 🟢 LOW (quick win) |

**Total hardcoded values outside of :root definitions: ~100**

---

*Note: The values in App.css `:root` are fine - those ARE the variable definitions. We're only worried about hardcoded values in the actual style rules.*
