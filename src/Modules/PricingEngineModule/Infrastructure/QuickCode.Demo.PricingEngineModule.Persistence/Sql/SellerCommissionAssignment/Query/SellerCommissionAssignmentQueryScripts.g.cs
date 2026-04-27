namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerCommissionAssignment
    {
        public static class Query
        {
            private const string _prefix = "PricingEngineModule.SellerCommissionAssignment.Query";
            private const string _sqlScriptStem = "SellerCommissionAssignment";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetByModelId => ResourceKey($"{_sqlScriptStem}.GetByModelId.g.sql");
        }
    }
}