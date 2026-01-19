namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiMethodAccessGrant
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.ApiMethodAccessGrant.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetApiMethodAccessGrantNames => ResourceKey("GetApiMethodAccessGrantNames.g.sql");
        }
    }
}