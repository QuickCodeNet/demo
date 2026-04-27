namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductCategory
    {
        public static class Command
        {
            private const string _prefix = "ProductCatalogModule.ProductCategory.Command";
            private const string _sqlScriptStem = "ProductCategory";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string RemoveFromCategory => ResourceKey($"{_sqlScriptStem}.RemoveFromCategory.g.sql");
        }
    }
}