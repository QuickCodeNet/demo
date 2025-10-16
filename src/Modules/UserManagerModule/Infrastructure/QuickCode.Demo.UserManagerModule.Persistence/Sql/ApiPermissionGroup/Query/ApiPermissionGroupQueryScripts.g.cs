namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiPermissionGroup
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.ApiPermissionGroup.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetApiPermissionGroupNames => ResourceKey("GetApiPermissionGroupNames.g.sql");
        }
    }
}