namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerPerformanceReview
    {
        public static class Query
        {
            private const string _prefix = "SellerManagementModule.SellerPerformanceReview.Query";
            private const string _sqlScriptStem = "SellerPerformanceReview";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetSellerAverageRating => ResourceKey($"{_sqlScriptStem}.GetSellerAverageRating.g.sql");
        }
    }
}