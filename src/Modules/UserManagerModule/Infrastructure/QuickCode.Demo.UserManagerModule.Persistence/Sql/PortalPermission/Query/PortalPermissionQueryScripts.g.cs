namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPermission
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.PortalPermission.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetPortalPermissionGroupsForPortalPermissions => ResourceKey("GetPortalPermissionGroupsForPortalPermissions.g.sql");
            public static string GetPortalPermissionGroupsForPortalPermissionsDetails => ResourceKey("GetPortalPermissionGroupsForPortalPermissionsDetails.g.sql");
        }
    }
}