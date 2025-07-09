namespace QuickCode.Demo.UserManagerModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class KafkaEvents
    {
        public static class Query
        {
            public const string GetKafkaEvents = "UserManagerModule.KafkaEvents.Query.GetKafkaEvents.g.sql";
            public const string GetActiveKafkaEvents = "UserManagerModule.KafkaEvents.Query.GetActiveKafkaEvents.g.sql";
            public const string GetTopicWorkflows = "UserManagerModule.KafkaEvents.Query.GetTopicWorkflows.g.sql";
            public const string GetTopicWorkflowsForKafkaEvents = "UserManagerModule.KafkaEvents.Query.GetTopicWorkflowsForKafkaEvents.g.sql";
            public const string GetTopicWorkflowsForKafkaEventsDetails = "UserManagerModule.KafkaEvents.Query.GetTopicWorkflowsForKafkaEventsDetails.g.sql";
        }
    }
}