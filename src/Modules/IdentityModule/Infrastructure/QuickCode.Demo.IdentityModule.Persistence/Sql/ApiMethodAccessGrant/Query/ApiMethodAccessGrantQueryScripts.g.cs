namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiMethodAccessGrant
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.ApiMethodAccessGrant.Query";
            private const string _sqlScriptStem = "ApiMethodAccessGrant";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetApiMethodAccessGrantNames => ResourceKey($"{_sqlScriptStem}.GetApiMethodAccessGrantNames.g.sql");
        }
    }
}