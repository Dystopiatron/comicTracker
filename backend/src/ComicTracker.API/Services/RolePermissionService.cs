using comicTracker.Models;

namespace comicTracker.Services
{
    public interface IRolePermissionService
    {
        bool HasPermission(UserRole role, Permission permission);
        IEnumerable<Permission> GetPermissions(UserRole role);
        IEnumerable<string> GetRoleDisplayNames();
        string GetRoleDisplayName(UserRole role);
    }

    public class RolePermissionService : IRolePermissionService
    {
        private readonly Dictionary<UserRole, HashSet<Permission>> _rolePermissions;

        public RolePermissionService()
        {
            _rolePermissions = new Dictionary<UserRole, HashSet<Permission>>
            {
                [UserRole.User] = new HashSet<Permission>
                {
                    Permission.ReadComics,
                    Permission.WriteComics,
                    Permission.DeleteComics
                },
                [UserRole.Moderator] = new HashSet<Permission>
                {
                    Permission.ReadComics,
                    Permission.WriteComics,
                    Permission.DeleteComics,
                    Permission.ReadUsers,
                    Permission.ViewStatistics
                },
                [UserRole.Admin] = new HashSet<Permission>
                {
                    Permission.ReadComics,
                    Permission.WriteComics,
                    Permission.DeleteComics,
                    Permission.ReadUsers,
                    Permission.WriteUsers,
                    Permission.DeleteUsers,
                    Permission.PromoteUsers,
                    Permission.ViewStatistics,
                    Permission.ManageSystem,
                    Permission.ViewLogs,
                    Permission.AccessAdminPanel
                },
                [UserRole.SuperAdmin] = new HashSet<Permission>
                {
                    Permission.ReadComics,
                    Permission.WriteComics,
                    Permission.DeleteComics,
                    Permission.ReadUsers,
                    Permission.WriteUsers,
                    Permission.DeleteUsers,
                    Permission.PromoteUsers,
                    Permission.ViewStatistics,
                    Permission.ManageSystem,
                    Permission.ViewLogs,
                    Permission.AccessAdminPanel,
                    Permission.ManageRoles
                }
            };
        }

        public bool HasPermission(UserRole role, Permission permission)
        {
            return _rolePermissions.ContainsKey(role) && _rolePermissions[role].Contains(permission);
        }

        public IEnumerable<Permission> GetPermissions(UserRole role)
        {
            return _rolePermissions.ContainsKey(role) ? _rolePermissions[role] : new HashSet<Permission>();
        }

        public IEnumerable<string> GetRoleDisplayNames()
        {
            return Enum.GetValues<UserRole>().Select(GetRoleDisplayName);
        }

        public string GetRoleDisplayName(UserRole role)
        {
            return role switch
            {
                UserRole.User => "User",
                UserRole.Moderator => "Moderator",
                UserRole.Admin => "Administrator",
                UserRole.SuperAdmin => "Super Administrator",
                _ => "Unknown"
            };
        }
    }
}