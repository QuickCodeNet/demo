namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Product
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.Product.Query";
            private const string _sqlScriptStem = "Product";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySku => ResourceKey($"{_sqlScriptStem}.GetBySku.g.sql");
            public static string GetActiveBySeller => ResourceKey($"{_sqlScriptStem}.GetActiveBySeller.g.sql");
            public static string SearchProducts => ResourceKey($"{_sqlScriptStem}.SearchProducts.g.sql");
            public static string GetPendingApproval => ResourceKey($"{_sqlScriptStem}.GetPendingApproval.g.sql");
            public static string GetFeatured => ResourceKey($"{_sqlScriptStem}.GetFeatured.g.sql");
            public static string GetProductsWithDetails => ResourceKey($"{_sqlScriptStem}.GetProductsWithDetails.g.sql");
            public static string GetCountByStatus => ResourceKey($"{_sqlScriptStem}.GetCountByStatus.g.sql");
        }
    }
}