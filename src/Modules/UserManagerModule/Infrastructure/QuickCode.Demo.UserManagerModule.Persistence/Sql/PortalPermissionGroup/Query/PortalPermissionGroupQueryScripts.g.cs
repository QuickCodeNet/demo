namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPermissionGroup
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.PortalPermissionGroup.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetPortalPermissionGroups => ResourceKey("GetPortalPermissionGroups.g.sql");
            public static string GetPortalPermissionGroup => ResourceKey("GetPortalPermissionGroup.g.sql");
        }
    }
}