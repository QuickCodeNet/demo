namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class EmailSender
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.EmailSender.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetInfoMessagesForEmailSenders => ResourceKey("GetInfoMessagesForEmailSenders.g.sql");
            public static string GetInfoMessagesForEmailSendersDetails => ResourceKey("GetInfoMessagesForEmailSendersDetails.g.sql");
            public static string GetOtpMessagesForEmailSenders => ResourceKey("GetOtpMessagesForEmailSenders.g.sql");
            public static string GetOtpMessagesForEmailSendersDetails => ResourceKey("GetOtpMessagesForEmailSendersDetails.g.sql");
            public static string GetCampaignMessagesForEmailSenders => ResourceKey("GetCampaignMessagesForEmailSenders.g.sql");
            public static string GetCampaignMessagesForEmailSendersDetails => ResourceKey("GetCampaignMessagesForEmailSendersDetails.g.sql");
        }
    }
}