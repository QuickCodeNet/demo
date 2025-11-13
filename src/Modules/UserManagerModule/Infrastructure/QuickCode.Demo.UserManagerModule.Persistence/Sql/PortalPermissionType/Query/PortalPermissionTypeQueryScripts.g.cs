namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPermissionType
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.PortalPermissionType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetPortalPermissionGroupsForPortalPermissionTypes => ResourceKey("GetPortalPermissionGroupsForPortalPermissionTypes.g.sql");
            public static string GetPortalPermissionGroupsForPortalPermissionTypesDetails => ResourceKey("GetPortalPermissionGroupsForPortalPermissionTypesDetails.g.sql");
        }
    }
}