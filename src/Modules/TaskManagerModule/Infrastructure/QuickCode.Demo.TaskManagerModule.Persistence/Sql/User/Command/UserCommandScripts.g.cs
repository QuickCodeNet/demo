namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class User
    {
        public static class Command
        {
            private const string _prefix = "TaskManagerModule.User.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ActivateUser => ResourceKey("ActivateUser.g.sql");
            public static string DeactivateUser => ResourceKey("DeactivateUser.g.sql");
        }
    }
}