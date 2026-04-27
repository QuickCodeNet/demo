namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerDocument
    {
        public static class Query
        {
            private const string _prefix = "SellerManagementModule.SellerDocument.Query";
            private const string _sqlScriptStem = "SellerDocument";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetPendingDocuments => ResourceKey($"{_sqlScriptStem}.GetPendingDocuments.g.sql");
        }
    }
}