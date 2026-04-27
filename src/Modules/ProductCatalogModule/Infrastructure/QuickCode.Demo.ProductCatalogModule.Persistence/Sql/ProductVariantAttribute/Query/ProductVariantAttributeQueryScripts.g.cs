namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductVariantAttribute
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.ProductVariantAttribute.Query";
            private const string _sqlScriptStem = "ProductVariantAttribute";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByVariantId => ResourceKey($"{_sqlScriptStem}.GetByVariantId.g.sql");
        }
    }
}