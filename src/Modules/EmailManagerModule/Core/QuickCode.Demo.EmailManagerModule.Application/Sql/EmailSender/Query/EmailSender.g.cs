namespace QuickCode.Demo.EmailManagerModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class EmailSender
    {
        public static class Query
        {
            public const string GetInfoMessagesForEmailSenders = "EmailManagerModule.EmailSender.Query.GetInfoMessagesForEmailSenders.g.sql";
            public const string GetInfoMessagesForEmailSendersDetails = "EmailManagerModule.EmailSender.Query.GetInfoMessagesForEmailSendersDetails.g.sql";
            public const string GetOtpMessagesForEmailSenders = "EmailManagerModule.EmailSender.Query.GetOtpMessagesForEmailSenders.g.sql";
            public const string GetOtpMessagesForEmailSendersDetails = "EmailManagerModule.EmailSender.Query.GetOtpMessagesForEmailSendersDetails.g.sql";
            public const string GetCampaignMessagesForEmailSenders = "EmailManagerModule.EmailSender.Query.GetCampaignMessagesForEmailSenders.g.sql";
            public const string GetCampaignMessagesForEmailSendersDetails = "EmailManagerModule.EmailSender.Query.GetCampaignMessagesForEmailSendersDetails.g.sql";
        }
    }
}