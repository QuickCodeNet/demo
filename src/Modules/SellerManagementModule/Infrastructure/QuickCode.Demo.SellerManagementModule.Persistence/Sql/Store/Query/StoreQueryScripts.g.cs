namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Store
    {
        public static class Query
        {
            private const string _prefix = "SellerManagementModule.Store.Query";
            private const string _sqlScriptStem = "Store";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetBySlug => ResourceKey($"{_sqlScriptStem}.GetBySlug.g.sql");
        }
    }
}