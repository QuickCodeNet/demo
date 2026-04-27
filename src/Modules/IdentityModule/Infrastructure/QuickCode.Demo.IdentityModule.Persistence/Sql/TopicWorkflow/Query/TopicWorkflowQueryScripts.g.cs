namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class TopicWorkflow
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.TopicWorkflow.Query";
            private const string _sqlScriptStem = "TopicWorkflow";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetWorkflows => ResourceKey($"{_sqlScriptStem}.GetWorkflows.g.sql");
            public static string GetWorkflows2 => ResourceKey($"{_sqlScriptStem}.GetWorkflows2.g.sql");
            public static string GetTopicWorkflows => ResourceKey($"{_sqlScriptStem}.GetTopicWorkflows.g.sql");
        }
    }
}