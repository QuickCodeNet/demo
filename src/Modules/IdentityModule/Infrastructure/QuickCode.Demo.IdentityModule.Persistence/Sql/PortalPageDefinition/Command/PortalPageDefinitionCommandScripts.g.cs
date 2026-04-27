namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPageDefinition
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.PortalPageDefinition.Command";
            private const string _sqlScriptStem = "PortalPageDefinition";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string DeletePortalPageDefinitionsWithModuleName => ResourceKey($"{_sqlScriptStem}.DeletePortalPageDefinitionsWithModuleName.g.sql");
            public static string DeletePortalPageDefinitionsWithModelName => ResourceKey($"{_sqlScriptStem}.DeletePortalPageDefinitionsWithModelName.g.sql");
        }
    }
}