namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class RefreshToken
    {
        public static class Command
        {
            private const string _prefix = "UserManagerModule.RefreshToken.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateRefreshTokens => ResourceKey("UpdateRefreshTokens.g.sql");
        }
    }
}