import React from 'react';
import UserProfile from '../components/Profile/UserProfile';

const ProfilePage = () => {
  return (
    <div className="container">
      <div className="content-wrapper">
        <div className="mb-4">
          <h1>User Profile</h1>
          <p className="text-muted">Manage your account information</p>
        </div>

        <UserProfile />
      </div>
    </div>
  );
};

export default ProfilePage;