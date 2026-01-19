namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessageQueue
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.OtpMessageQueue.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByOtpMessage => ResourceKey("GetByOtpMessage.g.sql");
            public static string GetPendingQueue => ResourceKey("GetPendingQueue.g.sql");
        }
    }
}