namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ShippingInfo
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.ShippingInfo.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetUserShippingInfo => ResourceKey("GetUserShippingInfo.g.sql");
            public static string GetOrdersForShippingInfos => ResourceKey("GetOrdersForShippingInfos.g.sql");
            public static string GetOrdersForShippingInfosDetails => ResourceKey("GetOrdersForShippingInfosDetails.g.sql");
        }
    }
}