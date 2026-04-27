namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CampaignApplicability
    {
        public static class Query
        {
            private const string _prefix = "PricingEngineModule.CampaignApplicability.Query";
            private const string _sqlScriptStem = "CampaignApplicability";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByCampaignId => ResourceKey($"{_sqlScriptStem}.GetByCampaignId.g.sql");
        }
    }
}