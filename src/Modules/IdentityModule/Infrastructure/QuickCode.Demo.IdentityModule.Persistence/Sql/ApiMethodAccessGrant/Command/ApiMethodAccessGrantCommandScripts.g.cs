namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiMethodAccessGrant
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.ApiMethodAccessGrant.Command";
            private const string _sqlScriptStem = "ApiMethodAccessGrant";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ClearApiMethodAccessGrants => ResourceKey($"{_sqlScriptStem}.ClearApiMethodAccessGrants.g.sql");
        }
    }
}