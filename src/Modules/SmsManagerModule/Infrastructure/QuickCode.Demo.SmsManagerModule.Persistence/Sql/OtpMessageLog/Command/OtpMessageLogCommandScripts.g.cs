namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessageLog
    {
        public static class Command
        {
            private const string _prefix = "SmsManagerModule.OtpMessageLog.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStatus => ResourceKey("UpdateStatus.g.sql");
        }
    }
}