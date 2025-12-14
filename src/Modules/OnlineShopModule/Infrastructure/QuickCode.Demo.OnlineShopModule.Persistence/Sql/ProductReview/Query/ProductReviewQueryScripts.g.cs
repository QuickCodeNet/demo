namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductReview
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.ProductReview.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetRecentReviews => ResourceKey("GetRecentReviews.g.sql");
            public static string GetProductReviews => ResourceKey("GetProductReviews.g.sql");
        }
    }
}