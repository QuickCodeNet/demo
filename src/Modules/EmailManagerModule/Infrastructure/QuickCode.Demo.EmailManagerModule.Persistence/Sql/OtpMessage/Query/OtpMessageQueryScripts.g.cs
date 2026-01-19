namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpMessage
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.OtpMessage.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByRecipient => ResourceKey("GetByRecipient.g.sql");
            public static string ExistsByHash => ResourceKey("ExistsByHash.g.sql");
            public static string GetOtpMessageQueuesForOtpMessages => ResourceKey("GetOtpMessageQueuesForOtpMessages.g.sql");
            public static string GetOtpMessageQueuesForOtpMessagesDetails => ResourceKey("GetOtpMessageQueuesForOtpMessagesDetails.g.sql");
            public static string GetOtpMessageLogsForOtpMessages => ResourceKey("GetOtpMessageLogsForOtpMessages.g.sql");
            public static string GetOtpMessageLogsForOtpMessagesDetails => ResourceKey("GetOtpMessageLogsForOtpMessagesDetails.g.sql");
        }
    }
}