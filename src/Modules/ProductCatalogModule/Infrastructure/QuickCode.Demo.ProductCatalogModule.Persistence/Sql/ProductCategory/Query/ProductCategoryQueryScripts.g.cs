namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductCategory
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.ProductCategory.Query";
            private const string _sqlScriptStem = "ProductCategory";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByProductId => ResourceKey($"{_sqlScriptStem}.GetByProductId.g.sql");
            public static string GetByCategoryId => ResourceKey($"{_sqlScriptStem}.GetByCategoryId.g.sql");
        }
    }
}