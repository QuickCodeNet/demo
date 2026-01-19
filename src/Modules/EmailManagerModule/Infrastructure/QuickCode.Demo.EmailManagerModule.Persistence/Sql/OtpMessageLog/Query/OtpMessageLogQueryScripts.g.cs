namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessageLog
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.OtpMessageLog.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByOtpMessage => ResourceKey("GetByOtpMessage.g.sql");
            public static string GetBySender => ResourceKey("GetBySender.g.sql");
            public static string GetByStatus => ResourceKey("GetByStatus.g.sql");
            public static string GetOtpLogsWithMessage => ResourceKey("GetOtpLogsWithMessage.g.sql");
        }
    }
}