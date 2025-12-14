namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessage
    {
        public static class Command
        {
            private const string _prefix = "SmsManagerModule.OtpMessage.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStatus => ResourceKey("UpdateStatus.g.sql");
            public static string IncrementAttempt => ResourceKey("IncrementAttempt.g.sql");
        }
    }
}