namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CategoryCommissionOverride
    {
        public static class Query
        {
            private const string _prefix = "PricingEngineModule.CategoryCommissionOverride.Query";
            private const string _sqlScriptStem = "CategoryCommissionOverride";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByCategoryId => ResourceKey($"{_sqlScriptStem}.GetByCategoryId.g.sql");
        }
    }
}