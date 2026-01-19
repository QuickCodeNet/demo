namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Campaign
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Campaign.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveCampaigns => ResourceKey("GetActiveCampaigns.g.sql");
            public static string GetCouponsForCampaigns => ResourceKey("GetCouponsForCampaigns.g.sql");
            public static string GetCouponsForCampaignsDetails => ResourceKey("GetCouponsForCampaignsDetails.g.sql");
        }
    }
}