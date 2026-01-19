namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PermissionGroup
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.PermissionGroup.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetApiMethodAccessGrantsForPermissionGroups => ResourceKey("GetApiMethodAccessGrantsForPermissionGroups.g.sql");
            public static string GetApiMethodAccessGrantsForPermissionGroupsDetails => ResourceKey("GetApiMethodAccessGrantsForPermissionGroupsDetails.g.sql");
            public static string GetPortalPageAccessGrantsForPermissionGroups => ResourceKey("GetPortalPageAccessGrantsForPermissionGroups.g.sql");
            public static string GetPortalPageAccessGrantsForPermissionGroupsDetails => ResourceKey("GetPortalPageAccessGrantsForPermissionGroupsDetails.g.sql");
            public static string GetAspNetUsersForPermissionGroups => ResourceKey("GetAspNetUsersForPermissionGroups.g.sql");
            public static string GetAspNetUsersForPermissionGroupsDetails => ResourceKey("GetAspNetUsersForPermissionGroupsDetails.g.sql");
        }
    }
}