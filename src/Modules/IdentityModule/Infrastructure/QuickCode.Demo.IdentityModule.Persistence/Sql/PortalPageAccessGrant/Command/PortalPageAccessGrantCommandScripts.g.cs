namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPageAccessGrant
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.PortalPageAccessGrant.Command";
            private const string _sqlScriptStem = "PortalPageAccessGrant";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ClearPortalPageAccessGrants => ResourceKey($"{_sqlScriptStem}.ClearPortalPageAccessGrants.g.sql");
        }
    }
}