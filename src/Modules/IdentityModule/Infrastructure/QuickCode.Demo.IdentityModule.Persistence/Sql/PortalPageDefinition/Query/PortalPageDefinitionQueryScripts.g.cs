namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPageDefinition
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.PortalPageDefinition.Query";
            private const string _sqlScriptStem = "PortalPageDefinition";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetPortalPageDefinitionsWithModuleName => ResourceKey($"{_sqlScriptStem}.GetPortalPageDefinitionsWithModuleName.g.sql");
            public static string GetPortalPageDefinitionsWithModelName => ResourceKey($"{_sqlScriptStem}.GetPortalPageDefinitionsWithModelName.g.sql");
            public static string ExistsPortalPageDefinitionsWithModuleName => ResourceKey($"{_sqlScriptStem}.ExistsPortalPageDefinitionsWithModuleName.g.sql");
            public static string ExistsPortalPageDefinitionsWithModelName => ResourceKey($"{_sqlScriptStem}.ExistsPortalPageDefinitionsWithModelName.g.sql");
        }
    }
}