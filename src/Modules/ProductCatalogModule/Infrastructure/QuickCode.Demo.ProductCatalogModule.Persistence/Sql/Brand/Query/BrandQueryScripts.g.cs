namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Brand
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.Brand.Query";
            private const string _sqlScriptStem = "Brand";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActive => ResourceKey($"{_sqlScriptStem}.GetActive.g.sql");
            public static string SearchByName => ResourceKey($"{_sqlScriptStem}.SearchByName.g.sql");
        }
    }
}