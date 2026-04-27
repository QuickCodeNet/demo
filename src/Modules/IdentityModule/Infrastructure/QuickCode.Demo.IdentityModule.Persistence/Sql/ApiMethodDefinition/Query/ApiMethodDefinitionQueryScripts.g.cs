namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiMethodDefinition
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.ApiMethodDefinition.Query";
            private const string _sqlScriptStem = "ApiMethodDefinition";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetKafkaEventsApiMethodDefinitions => ResourceKey($"{_sqlScriptStem}.GetKafkaEventsApiMethodDefinitions.g.sql");
            public static string GetApiMethodDefinitionsWithModuleName => ResourceKey($"{_sqlScriptStem}.GetApiMethodDefinitionsWithModuleName.g.sql");
            public static string GetApiMethodDefinitionsWithModelName => ResourceKey($"{_sqlScriptStem}.GetApiMethodDefinitionsWithModelName.g.sql");
            public static string ExistsApiMethodDefinitionsWithModuleName => ResourceKey($"{_sqlScriptStem}.ExistsApiMethodDefinitionsWithModuleName.g.sql");
            public static string ExistsApiMethodDefinitionsWithModelName => ResourceKey($"{_sqlScriptStem}.ExistsApiMethodDefinitionsWithModelName.g.sql");
        }
    }
}