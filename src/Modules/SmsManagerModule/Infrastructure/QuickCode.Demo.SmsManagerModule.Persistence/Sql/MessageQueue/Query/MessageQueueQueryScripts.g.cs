namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class MessageQueue
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.MessageQueue.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByCampaign => ResourceKey("GetByCampaign.g.sql");
            public static string GetPendingQueue => ResourceKey("GetPendingQueue.g.sql");
            public static string GetQueueCount => ResourceKey("GetQueueCount.g.sql");
            public static string GetQueueDetails => ResourceKey("GetQueueDetails.g.sql");
        }
    }
}