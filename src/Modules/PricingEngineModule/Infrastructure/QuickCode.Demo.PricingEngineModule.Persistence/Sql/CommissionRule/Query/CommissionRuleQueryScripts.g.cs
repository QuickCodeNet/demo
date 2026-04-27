namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CommissionRule
    {
        public static class Query
        {
            private const string _prefix = "PricingEngineModule.CommissionRule.Query";
            private const string _sqlScriptStem = "CommissionRule";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByModelId => ResourceKey($"{_sqlScriptStem}.GetByModelId.g.sql");
            public static string GetActiveRulesByModel => ResourceKey($"{_sqlScriptStem}.GetActiveRulesByModel.g.sql");
        }
    }
}