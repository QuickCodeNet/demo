namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductVariantAttribute
    {
        public static class Command
        {
            private const string _prefix = "ProductCatalogModule.ProductVariantAttribute.Command";
            private const string _sqlScriptStem = "ProductVariantAttribute";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string RemoveAttribute => ResourceKey($"{_sqlScriptStem}.RemoveAttribute.g.sql");
        }
    }
}