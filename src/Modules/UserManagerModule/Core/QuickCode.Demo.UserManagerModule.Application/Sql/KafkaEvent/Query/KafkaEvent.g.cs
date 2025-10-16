namespace QuickCode.Demo.UserManagerModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class KafkaEvent
    {
        public static class Query
        {
            public const string GetKafkaEvents = "UserManagerModule.KafkaEvent.Query.GetKafkaEvents.g.sql";
            public const string GetActiveKafkaEvents = "UserManagerModule.KafkaEvent.Query.GetActiveKafkaEvents.g.sql";
            public const string GetTopicWorkflows = "UserManagerModule.KafkaEvent.Query.GetTopicWorkflows.g.sql";
            public const string GetTopicWorkflowsForKafkaEvents = "UserManagerModule.KafkaEvent.Query.GetTopicWorkflowsForKafkaEvents.g.sql";
            public const string GetTopicWorkflowsForKafkaEventsDetails = "UserManagerModule.KafkaEvent.Query.GetTopicWorkflowsForKafkaEventsDetails.g.sql";
        }
    }
}