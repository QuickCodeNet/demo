namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Coupon
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Coupon.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetValidCoupons => ResourceKey("GetValidCoupons.g.sql");
            public static string GetUserCoupons => ResourceKey("GetUserCoupons.g.sql");
            public static string GetUserCouponsForCoupons => ResourceKey("GetUserCouponsForCoupons.g.sql");
            public static string GetUserCouponsForCouponsDetails => ResourceKey("GetUserCouponsForCouponsDetails.g.sql");
        }
    }
}