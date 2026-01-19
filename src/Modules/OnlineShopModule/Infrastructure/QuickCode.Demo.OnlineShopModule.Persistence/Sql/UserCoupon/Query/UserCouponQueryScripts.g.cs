namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class UserCoupon
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.UserCoupon.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetUsedCoupons => ResourceKey("GetUsedCoupons.g.sql");
        }
    }
}