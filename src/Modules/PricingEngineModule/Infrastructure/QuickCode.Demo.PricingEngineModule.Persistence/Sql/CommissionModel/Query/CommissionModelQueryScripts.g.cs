namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CommissionModel
    {
        public static class Query
        {
            private const string _prefix = "PricingEngineModule.CommissionModel.Query";
            private const string _sqlScriptStem = "CommissionModel";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActive => ResourceKey($"{_sqlScriptStem}.GetActive.g.sql");
            public static string GetByName => ResourceKey($"{_sqlScriptStem}.GetByName.g.sql");
        }
    }
}