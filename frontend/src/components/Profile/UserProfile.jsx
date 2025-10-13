import React, { useState } from 'react';
import { useAuth } from '../../context/AuthContext';
import EditProfile from './EditProfile';
import Button from '../Common/Button';

const UserProfile = () => {
  const { user, updateUser } = useAuth();
  const [isEditing, setIsEditing] = useState(false);

  if (!user) {
    return (
      <div className="card">
        <p>Unable to load user profile.</p>
      </div>
    );
  }

  const handleSaveProfile = (updatedUser) => {
    // Update the auth context with the new user data
    updateUser(updatedUser);
    setIsEditing(false);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
  };

  if (isEditing) {
    const userData = user.data || user;
    return (
      <EditProfile
        user={userData}
        onSave={handleSaveProfile}
        onCancel={handleCancelEdit}
      />
    );
  }

  return (
    <div className="card">
      <div className="card-header d-flex justify-content-between align-items-center">
        <h3>Profile Information</h3>
        <Button variant="outline" onClick={() => setIsEditing(true)}>
          Edit Profile
        </Button>
      </div>

      <div className="profile-info">
        <div className="row mb-3" style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem' }}>
          <div>
            <label className="form-label text-muted">First Name</label>
            <p style={{ margin: 0, fontWeight: '500' }}>{user.data?.firstName || user.firstName || 'Not set'}</p>
          </div>
          <div>
            <label className="form-label text-muted">Last Name</label>
            <p style={{ margin: 0, fontWeight: '500' }}>{user.data?.lastName || user.lastName || 'Not set'}</p>
          </div>
        </div>

        <div className="row mb-3" style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem' }}>
          <div>
            <label className="form-label text-muted">Username</label>
            <p style={{ margin: 0, fontWeight: '500' }}>{user.data?.username || user.username || 'Not set'}</p>
          </div>
          <div>
            <label className="form-label text-muted">Email</label>
            <p style={{ margin: 0, fontWeight: '500' }}>{user.data?.email || user.email || 'Not set'}</p>
          </div>
        </div>

        <div className="mb-3">
          <label className="form-label text-muted">User ID</label>
          <p className="text-muted" style={{ margin: 0, fontSize: '0.875rem', fontFamily: 'monospace' }}>
            {user.data?.id || user.id || 'Not set'}
          </p>
        </div>
      </div>
    </div>
  );
};

export default UserProfile;