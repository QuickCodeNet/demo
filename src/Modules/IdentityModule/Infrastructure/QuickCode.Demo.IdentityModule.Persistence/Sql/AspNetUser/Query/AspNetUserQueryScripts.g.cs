namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class AspNetUser
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.AspNetUser.Query";
            private const string _sqlScriptStem = "AspNetUser";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetUser => ResourceKey($"{_sqlScriptStem}.GetUser.g.sql");
            public static string IdentityUserById => ResourceKey($"{_sqlScriptStem}.IdentityUserById.g.sql");
            public static string IdentityUserByNormalizedEmail => ResourceKey($"{_sqlScriptStem}.IdentityUserByNormalizedEmail.g.sql");
            public static string IdentityUserByNormalizedUsername => ResourceKey($"{_sqlScriptStem}.IdentityUserByNormalizedUsername.g.sql");
        }
    }
}