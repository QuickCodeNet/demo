namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PromotionalCampaign
    {
        public static class Query
        {
            private const string _prefix = "PricingEngineModule.PromotionalCampaign.Query";
            private const string _sqlScriptStem = "PromotionalCampaign";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveCampaigns => ResourceKey($"{_sqlScriptStem}.GetActiveCampaigns.g.sql");
            public static string GetCampaignsInDateRange => ResourceKey($"{_sqlScriptStem}.GetCampaignsInDateRange.g.sql");
        }
    }
}