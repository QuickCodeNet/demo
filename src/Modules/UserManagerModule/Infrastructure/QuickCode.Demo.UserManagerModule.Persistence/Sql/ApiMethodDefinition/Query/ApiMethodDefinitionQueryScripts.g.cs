namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiMethodDefinition
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.ApiMethodDefinition.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetApiMethodDefinitionsWithModuleName => ResourceKey("GetApiMethodDefinitionsWithModuleName.g.sql");
            public static string GetApiMethodDefinitionsWithModelName => ResourceKey("GetApiMethodDefinitionsWithModelName.g.sql");
            public static string ExistsApiMethodDefinitionsWithModuleName => ResourceKey("ExistsApiMethodDefinitionsWithModuleName.g.sql");
            public static string ExistsApiMethodDefinitionsWithModelName => ResourceKey("ExistsApiMethodDefinitionsWithModelName.g.sql");
            public static string GetApiMethodAccessGrantsForApiMethodDefinitions => ResourceKey("GetApiMethodAccessGrantsForApiMethodDefinitions.g.sql");
            public static string GetApiMethodAccessGrantsForApiMethodDefinitionsDetails => ResourceKey("GetApiMethodAccessGrantsForApiMethodDefinitionsDetails.g.sql");
            public static string GetKafkaEventsForApiMethodDefinitions => ResourceKey("GetKafkaEventsForApiMethodDefinitions.g.sql");
            public static string GetKafkaEventsForApiMethodDefinitionsDetails => ResourceKey("GetKafkaEventsForApiMethodDefinitionsDetails.g.sql");
        }
    }
}