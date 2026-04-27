namespace QuickCode.Demo.ProductCatalogModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class AttributeValue
    {
        public static class Query
        {
            private const string _prefix = "ProductCatalogModule.AttributeValue.Query";
            private const string _sqlScriptStem = "AttributeValue";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByAttributeId => ResourceKey($"{_sqlScriptStem}.GetByAttributeId.g.sql");
        }
    }
}