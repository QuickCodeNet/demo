namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApiMethodDefinition
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.ApiMethodDefinition.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetKafkaEventsForApiMethodDefinitions => ResourceKey("GetKafkaEventsForApiMethodDefinitions.g.sql");
            public static string GetKafkaEventsForApiMethodDefinitionsDetails => ResourceKey("GetKafkaEventsForApiMethodDefinitionsDetails.g.sql");
            public static string GetApiPermissionGroupsForApiMethodDefinitions => ResourceKey("GetApiPermissionGroupsForApiMethodDefinitions.g.sql");
            public static string GetApiPermissionGroupsForApiMethodDefinitionsDetails => ResourceKey("GetApiPermissionGroupsForApiMethodDefinitionsDetails.g.sql");
        }
    }
}