namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SmsSender
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.SmsSender.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetInfoMessagesForSmsSenders => ResourceKey("GetInfoMessagesForSmsSenders.g.sql");
            public static string GetInfoMessagesForSmsSendersDetails => ResourceKey("GetInfoMessagesForSmsSendersDetails.g.sql");
            public static string GetOtpMessagesForSmsSenders => ResourceKey("GetOtpMessagesForSmsSenders.g.sql");
            public static string GetOtpMessagesForSmsSendersDetails => ResourceKey("GetOtpMessagesForSmsSendersDetails.g.sql");
            public static string GetCampaignMessagesForSmsSenders => ResourceKey("GetCampaignMessagesForSmsSenders.g.sql");
            public static string GetCampaignMessagesForSmsSendersDetails => ResourceKey("GetCampaignMessagesForSmsSendersDetails.g.sql");
        }
    }
}