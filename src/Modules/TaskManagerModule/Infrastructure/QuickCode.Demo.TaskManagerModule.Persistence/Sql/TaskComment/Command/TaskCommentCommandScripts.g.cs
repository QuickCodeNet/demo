namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class TaskComment
    {
        public static class Command
        {
            private const string _prefix = "TaskManagerModule.TaskComment.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string DeleteComment => ResourceKey("DeleteComment.g.sql");
        }
    }
}