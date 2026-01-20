namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Task
    {
        public static class Command
        {
            private const string _prefix = "TaskManagerModule.Task.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateTaskStatus => ResourceKey("UpdateTaskStatus.g.sql");
            public static string UpdateTaskPriority => ResourceKey("UpdateTaskPriority.g.sql");
            public static string DeleteTask => ResourceKey("DeleteTask.g.sql");
        }
    }
}