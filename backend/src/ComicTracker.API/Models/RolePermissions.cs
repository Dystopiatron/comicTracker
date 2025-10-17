namespace comicTracker.Models
{
    public enum UserRole
    {
        User = 0,
        Moderator = 1,
        Admin = 2,
        SuperAdmin = 3
    }

    public enum Permission
    {
        // Comic permissions
        ReadComics = 0,
        WriteComics = 1,
        DeleteComics = 2,
        
        // User permissions
        ReadUsers = 10,
        WriteUsers = 11,
        DeleteUsers = 12,
        PromoteUsers = 13,
        
        // System permissions
        ViewStatistics = 20,
        ManageSystem = 21,
        ViewLogs = 22,
        
        // Admin permissions
        ManageRoles = 30,
        AccessAdminPanel = 31
    }
}