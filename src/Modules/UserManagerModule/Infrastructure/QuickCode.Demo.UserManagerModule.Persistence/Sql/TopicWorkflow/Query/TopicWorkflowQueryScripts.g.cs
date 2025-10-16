﻿namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class TopicWorkflow
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.TopicWorkflow.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetWorkflows => ResourceKey("GetWorkflows.g.sql");
            public static string GetWORKFLOWS2 => ResourceKey("GetWORKFLOWS2.g.sql");
            public static string GetTopicWorkflows => ResourceKey("GetTopicWorkflows.g.sql");
        }
    }
}