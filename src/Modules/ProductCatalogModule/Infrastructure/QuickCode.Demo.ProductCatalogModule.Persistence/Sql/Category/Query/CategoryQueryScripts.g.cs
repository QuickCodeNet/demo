namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Category
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.Category.Query";
            private const string _sqlScriptStem = "Category";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActive => ResourceKey($"{_sqlScriptStem}.GetActive.g.sql");
            public static string GetSubCategories => ResourceKey($"{_sqlScriptStem}.GetSubCategories.g.sql");
            public static string GetBySlug => ResourceKey($"{_sqlScriptStem}.GetBySlug.g.sql");
        }
    }
}