namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class MessageLog
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.MessageLog.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByMessage => ResourceKey("GetByMessage.g.sql");
            public static string GetByCampaign => ResourceKey("GetByCampaign.g.sql");
            public static string GetBySender => ResourceKey("GetBySender.g.sql");
            public static string GetLogsCount => ResourceKey("GetLogsCount.g.sql");
            public static string GetLogsWithSender => ResourceKey("GetLogsWithSender.g.sql");
        }
    }
}