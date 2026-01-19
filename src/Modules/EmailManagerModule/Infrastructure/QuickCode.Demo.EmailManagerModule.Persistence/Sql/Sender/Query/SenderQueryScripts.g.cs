namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Sender
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.Sender.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetActiveSenders => ResourceKey("GetActiveSenders.g.sql");
            public static string GetByName => ResourceKey("GetByName.g.sql");
            public static string ExistsByEmail => ResourceKey("ExistsByEmail.g.sql");
            public static string GetMessageLogsForSenders => ResourceKey("GetMessageLogsForSenders.g.sql");
            public static string GetMessageLogsForSendersDetails => ResourceKey("GetMessageLogsForSendersDetails.g.sql");
            public static string GetOtpMessageLogsForSenders => ResourceKey("GetOtpMessageLogsForSenders.g.sql");
            public static string GetOtpMessageLogsForSendersDetails => ResourceKey("GetOtpMessageLogsForSendersDetails.g.sql");
            public static string GetMessageQueuesForSenders => ResourceKey("GetMessageQueuesForSenders.g.sql");
            public static string GetMessageQueuesForSendersDetails => ResourceKey("GetMessageQueuesForSendersDetails.g.sql");
            public static string GetOtpMessageQueuesForSenders => ResourceKey("GetOtpMessageQueuesForSenders.g.sql");
            public static string GetOtpMessageQueuesForSendersDetails => ResourceKey("GetOtpMessageQueuesForSendersDetails.g.sql");
        }
    }
}