namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class MessageTemplate
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.MessageTemplate.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByName => ResourceKey("GetByName.g.sql");
            public static string GetByType => ResourceKey("GetByType.g.sql");
            public static string ExistsByName => ResourceKey("ExistsByName.g.sql");
            public static string GetCampaignsForMessageTemplates => ResourceKey("GetCampaignsForMessageTemplates.g.sql");
            public static string GetCampaignsForMessageTemplatesDetails => ResourceKey("GetCampaignsForMessageTemplatesDetails.g.sql");
            public static string GetMessagesForMessageTemplates => ResourceKey("GetMessagesForMessageTemplates.g.sql");
            public static string GetMessagesForMessageTemplatesDetails => ResourceKey("GetMessagesForMessageTemplatesDetails.g.sql");
            public static string GetOtpMessagesForMessageTemplates => ResourceKey("GetOtpMessagesForMessageTemplates.g.sql");
            public static string GetOtpMessagesForMessageTemplatesDetails => ResourceKey("GetOtpMessagesForMessageTemplatesDetails.g.sql");
            public static string GetOtpMessageLogsForMessageTemplates => ResourceKey("GetOtpMessageLogsForMessageTemplates.g.sql");
            public static string GetOtpMessageLogsForMessageTemplatesDetails => ResourceKey("GetOtpMessageLogsForMessageTemplatesDetails.g.sql");
            public static string GetMessageLogsForMessageTemplates => ResourceKey("GetMessageLogsForMessageTemplates.g.sql");
            public static string GetMessageLogsForMessageTemplatesDetails => ResourceKey("GetMessageLogsForMessageTemplatesDetails.g.sql");
        }
    }
}