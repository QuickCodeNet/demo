namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Sender
    {
        public static class Command
        {
            private const string _prefix = "EmailManagerModule.Sender.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStatus => ResourceKey("UpdateStatus.g.sql");
            public static string UpdatePriority => ResourceKey("UpdatePriority.g.sql");
            public static string UpdateDailyLimit => ResourceKey("UpdateDailyLimit.g.sql");
        }
    }
}