namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class KafkaEvent
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.KafkaEvent.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetKafkaEvents => ResourceKey("GetKafkaEvents.g.sql");
            public static string GetActiveKafkaEvents => ResourceKey("GetActiveKafkaEvents.g.sql");
            public static string GetTopicWorkflows => ResourceKey("GetTopicWorkflows.g.sql");
            public static string GetTopicWorkflowsForKafkaEvents => ResourceKey("GetTopicWorkflowsForKafkaEvents.g.sql");
            public static string GetTopicWorkflowsForKafkaEventsDetails => ResourceKey("GetTopicWorkflowsForKafkaEventsDetails.g.sql");
        }
    }
}