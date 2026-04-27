namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductVariant
    {
        public static class Command
        {
            private const string _prefix = "ProductCatalogModule.ProductVariant.Command";
            private const string _sqlScriptStem = "ProductVariant";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStock => ResourceKey($"{_sqlScriptStem}.UpdateStock.g.sql");
            public static string UpdatePrice => ResourceKey($"{_sqlScriptStem}.UpdatePrice.g.sql");
            public static string Deactivate => ResourceKey($"{_sqlScriptStem}.Deactivate.g.sql");
        }
    }
}