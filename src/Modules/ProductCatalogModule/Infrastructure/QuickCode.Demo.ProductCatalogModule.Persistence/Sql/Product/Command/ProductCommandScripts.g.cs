namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Product
    {
        public static class Command
        {
            private const string _prefix = "ProductCatalogModule.Product.Command";
            private const string _sqlScriptStem = "Product";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Approve => ResourceKey($"{_sqlScriptStem}.Approve.g.sql");
            public static string Reject => ResourceKey($"{_sqlScriptStem}.Reject.g.sql");
            public static string SetFeatured => ResourceKey($"{_sqlScriptStem}.SetFeatured.g.sql");
        }
    }
}