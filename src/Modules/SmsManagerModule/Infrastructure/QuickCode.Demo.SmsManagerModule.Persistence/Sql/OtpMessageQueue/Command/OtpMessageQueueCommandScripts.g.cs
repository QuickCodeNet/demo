namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessageQueue
    {
        public static class Command
        {
            private const string _prefix = "SmsManagerModule.OtpMessageQueue.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStatus => ResourceKey("UpdateStatus.g.sql");
            public static string UpdatePriority => ResourceKey("UpdatePriority.g.sql");
        }
    }
}