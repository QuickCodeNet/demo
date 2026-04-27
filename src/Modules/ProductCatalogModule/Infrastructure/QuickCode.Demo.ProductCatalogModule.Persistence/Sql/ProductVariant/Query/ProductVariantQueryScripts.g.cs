namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductVariant
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.ProductVariant.Query";
            private const string _sqlScriptStem = "ProductVariant";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByProductId => ResourceKey($"{_sqlScriptStem}.GetByProductId.g.sql");
            public static string GetActiveByProductId => ResourceKey($"{_sqlScriptStem}.GetActiveByProductId.g.sql");
            public static string GetLowStockVariants => ResourceKey($"{_sqlScriptStem}.GetLowStockVariants.g.sql");
        }
    }
}