namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessageLog
    {
        public static class Command
        {
            private const string _prefix = "EmailManagerModule.OtpMessageLog.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStatus => ResourceKey("UpdateStatus.g.sql");
        }
    }
}