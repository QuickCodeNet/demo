namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Coupon
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.Coupon.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateUserCoupons => ResourceKey("UpdateUserCoupons.g.sql");
            public static string UpdateUserCouponsPrm => ResourceKey("UpdateUserCouponsPrm.g.sql");
            public static string UpdateUserCouponsSingle => ResourceKey("UpdateUserCouponsSingle.g.sql");
            public static string UpdateUserCouponsWherePrm => ResourceKey("UpdateUserCouponsWherePrm.g.sql");
            public static string UpdateUserCouponsRefCol => ResourceKey("UpdateUserCouponsRefCol.g.sql");
        }
    }
}