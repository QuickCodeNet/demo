namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class RefreshToken
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.RefreshToken.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetRefreshToken => ResourceKey("GetRefreshToken.g.sql");
        }
    }
}