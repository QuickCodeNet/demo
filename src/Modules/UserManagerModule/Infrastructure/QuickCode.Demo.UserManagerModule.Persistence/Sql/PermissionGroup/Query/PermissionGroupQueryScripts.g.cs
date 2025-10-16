namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PermissionGroup
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.PermissionGroup.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetAspNetUsersForPermissionGroups => ResourceKey("GetAspNetUsersForPermissionGroups.g.sql");
            public static string GetAspNetUsersForPermissionGroupsDetails => ResourceKey("GetAspNetUsersForPermissionGroupsDetails.g.sql");
            public static string GetPortalPermissionGroupsForPermissionGroups => ResourceKey("GetPortalPermissionGroupsForPermissionGroups.g.sql");
            public static string GetPortalPermissionGroupsForPermissionGroupsDetails => ResourceKey("GetPortalPermissionGroupsForPermissionGroupsDetails.g.sql");
            public static string GetApiPermissionGroupsForPermissionGroups => ResourceKey("GetApiPermissionGroupsForPermissionGroups.g.sql");
            public static string GetApiPermissionGroupsForPermissionGroupsDetails => ResourceKey("GetApiPermissionGroupsForPermissionGroupsDetails.g.sql");
        }
    }
}