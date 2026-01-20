namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class User
    {
        public static class Query
        {
            private const string _prefix = "TaskManagerModule.User.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveUsers => ResourceKey("GetActiveUsers.g.sql");
            public static string GetUserByUsername => ResourceKey("GetUserByUsername.g.sql");
            public static string GetUserByEmail => ResourceKey("GetUserByEmail.g.sql");
            public static string ExistsByUsername => ResourceKey("ExistsByUsername.g.sql");
            public static string ExistsByEmail => ResourceKey("ExistsByEmail.g.sql");
            public static string GetTasksForUsers => ResourceKey("GetTasksForUsers.g.sql");
            public static string GetTasksForUsersDetails => ResourceKey("GetTasksForUsersDetails.g.sql");
            public static string GetProjectsForUsers => ResourceKey("GetProjectsForUsers.g.sql");
            public static string GetProjectsForUsersDetails => ResourceKey("GetProjectsForUsersDetails.g.sql");
            public static string GetTaskCommentsForUsers => ResourceKey("GetTaskCommentsForUsers.g.sql");
            public static string GetTaskCommentsForUsersDetails => ResourceKey("GetTaskCommentsForUsersDetails.g.sql");
        }
    }
}