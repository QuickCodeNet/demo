namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CampaignType
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.CampaignType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetCampaignMessagesForCampaignTypes => ResourceKey("GetCampaignMessagesForCampaignTypes.g.sql");
            public static string GetCampaignMessagesForCampaignTypesDetails => ResourceKey("GetCampaignMessagesForCampaignTypesDetails.g.sql");
        }
    }
}