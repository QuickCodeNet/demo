namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Order
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Order.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetOrdersYearly => ResourceKey("GetOrdersYearly.g.sql");
            public static string GetOrdersByStatusDate => ResourceKey("GetOrdersByStatusDate.g.sql");
            public static string GetUserOrders => ResourceKey("GetUserOrders.g.sql");
            public static string GetOrderItemsForOrders => ResourceKey("GetOrderItemsForOrders.g.sql");
            public static string GetOrderItemsForOrdersDetails => ResourceKey("GetOrderItemsForOrdersDetails.g.sql");
            public static string GetShipmentsForOrders => ResourceKey("GetShipmentsForOrders.g.sql");
            public static string GetShipmentsForOrdersDetails => ResourceKey("GetShipmentsForOrdersDetails.g.sql");
        }
    }
}