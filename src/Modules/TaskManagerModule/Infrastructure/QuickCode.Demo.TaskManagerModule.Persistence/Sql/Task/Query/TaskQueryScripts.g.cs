namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Task
    {
        public static class Query
        {
            private const string _prefix = "TaskManagerModule.Task.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetTasksByUserId => ResourceKey("GetTasksByUserId.g.sql");
            public static string GetTasksByStatus => ResourceKey("GetTasksByStatus.g.sql");
            public static string GetTasksByPriority => ResourceKey("GetTasksByPriority.g.sql");
            public static string GetTasksDueToday => ResourceKey("GetTasksDueToday.g.sql");
            public static string SearchTasks => ResourceKey("SearchTasks.g.sql");
            public static string GetTaskCommentsForTasks => ResourceKey("GetTaskCommentsForTasks.g.sql");
            public static string GetTaskCommentsForTasksDetails => ResourceKey("GetTaskCommentsForTasksDetails.g.sql");
        }
    }
}