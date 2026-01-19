namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Message
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.Message.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByCampaign => ResourceKey("GetByCampaign.g.sql");
            public static string GetByStatus => ResourceKey("GetByStatus.g.sql");
            public static string GetPendingMessages => ResourceKey("GetPendingMessages.g.sql");
            public static string GetMessagesCount => ResourceKey("GetMessagesCount.g.sql");
            public static string GetMessagesWithCampaign => ResourceKey("GetMessagesWithCampaign.g.sql");
            public static string GetMessageQueuesForMessages => ResourceKey("GetMessageQueuesForMessages.g.sql");
            public static string GetMessageQueuesForMessagesDetails => ResourceKey("GetMessageQueuesForMessagesDetails.g.sql");
            public static string GetMessageLogsForMessages => ResourceKey("GetMessageLogsForMessages.g.sql");
            public static string GetMessageLogsForMessagesDetails => ResourceKey("GetMessageLogsForMessagesDetails.g.sql");
        }
    }
}