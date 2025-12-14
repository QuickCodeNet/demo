namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ShippingInfo
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.ShippingInfo.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string SetDefaultShipping => ResourceKey("SetDefaultShipping.g.sql");
            public static string SetDefaultShippingAddress => ResourceKey("SetDefaultShippingAddress.g.sql");
        }
    }
}