namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class KafkaEvent
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.KafkaEvent.Query";
            private const string _sqlScriptStem = "KafkaEvent";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetKafkaEvents => ResourceKey($"{_sqlScriptStem}.GetKafkaEvents.g.sql");
            public static string GetActiveKafkaEvents => ResourceKey($"{_sqlScriptStem}.GetActiveKafkaEvents.g.sql");
            public static string GetTopicWorkflows => ResourceKey($"{_sqlScriptStem}.GetTopicWorkflows.g.sql");
            public static string GetTopicWorkflowsKafkaEvents => ResourceKey($"{_sqlScriptStem}.GetTopicWorkflowsKafkaEvents.g.sql");
        }
    }
}