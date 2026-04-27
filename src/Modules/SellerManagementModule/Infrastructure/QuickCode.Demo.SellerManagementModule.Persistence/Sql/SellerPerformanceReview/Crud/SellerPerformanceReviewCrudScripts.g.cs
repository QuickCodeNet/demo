namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerPerformanceReview
    {
        public static class Crud
        {
            private const string _prefix = "SellerManagementModule.SellerPerformanceReview.Crud";
            private const string _sqlScriptStem = "SellerPerformanceReview";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Insert => ResourceKey($"{_sqlScriptStem}.Insert.g.sql");
            public static string Update => ResourceKey($"{_sqlScriptStem}.Update.g.sql");
            public static string Delete => ResourceKey($"{_sqlScriptStem}.Delete.g.sql");
            public static string GetByPk => ResourceKey($"{_sqlScriptStem}.GetByPk.g.sql");
            public static string List => ResourceKey($"{_sqlScriptStem}.List.g.sql");
            public static string Count => ResourceKey($"{_sqlScriptStem}.Count.g.sql");
        }
    }
}