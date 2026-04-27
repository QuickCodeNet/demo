namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class RefreshToken
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.RefreshToken.Query";
            private const string _sqlScriptStem = "RefreshToken";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetRefreshToken => ResourceKey($"{_sqlScriptStem}.GetRefreshToken.g.sql");
        }
    }
}