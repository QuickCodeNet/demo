namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class UserCoupon
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.UserCoupon.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateUsedCoupons => ResourceKey("UpdateUsedCoupons.g.sql");
            public static string UpdateAllUsedCoupons => ResourceKey("UpdateAllUsedCoupons.g.sql");
            public static string UpdateAllTrueUsedCoupons => ResourceKey("UpdateAllTrueUsedCoupons.g.sql");
        }
    }
}