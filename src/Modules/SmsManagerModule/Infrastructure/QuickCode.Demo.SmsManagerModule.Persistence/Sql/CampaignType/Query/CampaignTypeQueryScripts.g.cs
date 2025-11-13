namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CampaignType
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.CampaignType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetCampaignMessagesForCampaignTypes => ResourceKey("GetCampaignMessagesForCampaignTypes.g.sql");
            public static string GetCampaignMessagesForCampaignTypesDetails => ResourceKey("GetCampaignMessagesForCampaignTypesDetails.g.sql");
        }
    }
}