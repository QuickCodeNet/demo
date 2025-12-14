namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class User
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.User.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetNewUsers => ResourceKey("GetNewUsers.g.sql");
            public static string GetCouponsForUsers => ResourceKey("GetCouponsForUsers.g.sql");
            public static string GetCouponsForUsersDetails => ResourceKey("GetCouponsForUsersDetails.g.sql");
            public static string GetUserCouponsForUsers => ResourceKey("GetUserCouponsForUsers.g.sql");
            public static string GetUserCouponsForUsersDetails => ResourceKey("GetUserCouponsForUsersDetails.g.sql");
            public static string GetProductReviewsForUsers => ResourceKey("GetProductReviewsForUsers.g.sql");
            public static string GetProductReviewsForUsersDetails => ResourceKey("GetProductReviewsForUsersDetails.g.sql");
            public static string GetCartsForUsers => ResourceKey("GetCartsForUsers.g.sql");
            public static string GetCartsForUsersDetails => ResourceKey("GetCartsForUsersDetails.g.sql");
            public static string GetOrdersForUsers => ResourceKey("GetOrdersForUsers.g.sql");
            public static string GetOrdersForUsersDetails => ResourceKey("GetOrdersForUsersDetails.g.sql");
        }
    }
}