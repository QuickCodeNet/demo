namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Attribute
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.Attribute.Query";
            private const string _sqlScriptStem = "Attribute";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByCode => ResourceKey($"{_sqlScriptStem}.GetByCode.g.sql");
        }
    }
}