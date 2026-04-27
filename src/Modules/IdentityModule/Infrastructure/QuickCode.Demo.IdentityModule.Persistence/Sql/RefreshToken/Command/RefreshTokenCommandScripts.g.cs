namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class RefreshToken
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.RefreshToken.Command";
            private const string _sqlScriptStem = "RefreshToken";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateRefreshTokens => ResourceKey($"{_sqlScriptStem}.UpdateRefreshTokens.g.sql");
        }
    }
}