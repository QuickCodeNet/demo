namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OrderItem
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.OrderItem.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetOrderItemsDetails => ResourceKey("GetOrderItemsDetails.g.sql");
            public static string GetProductOrderItems => ResourceKey("GetProductOrderItems.g.sql");
        }
    }
}