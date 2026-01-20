namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class TaskComment
    {
        public static class Query
        {
            private const string _prefix = "TaskManagerModule.TaskComment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetCommentsForTask => ResourceKey("GetCommentsForTask.g.sql");
        }
    }
}