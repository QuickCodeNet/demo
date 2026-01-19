namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Campaign
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.Campaign.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetActiveCampaigns => ResourceKey("GetActiveCampaigns.g.sql");
            public static string GetByName => ResourceKey("GetByName.g.sql");
            public static string ExistsByName => ResourceKey("ExistsByName.g.sql");
            public static string GetCampaignsCount => ResourceKey("GetCampaignsCount.g.sql");
            public static string GetMessagesForCampaigns => ResourceKey("GetMessagesForCampaigns.g.sql");
            public static string GetMessagesForCampaignsDetails => ResourceKey("GetMessagesForCampaignsDetails.g.sql");
            public static string GetMessageQueuesForCampaigns => ResourceKey("GetMessageQueuesForCampaigns.g.sql");
            public static string GetMessageQueuesForCampaignsDetails => ResourceKey("GetMessageQueuesForCampaignsDetails.g.sql");
        }
    }
}