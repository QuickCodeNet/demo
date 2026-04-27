namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PromotionalCampaign
    {
        public static class Command
        {
            private const string _prefix = "PricingEngineModule.PromotionalCampaign.Command";
            private const string _sqlScriptStem = "PromotionalCampaign";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Deactivate => ResourceKey($"{_sqlScriptStem}.Deactivate.g.sql");
        }
    }
}