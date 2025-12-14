namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Cart
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Cart.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetAbandonedCarts => ResourceKey("GetAbandonedCarts.g.sql");
            public static string GetCartItemsForCarts => ResourceKey("GetCartItemsForCarts.g.sql");
            public static string GetCartItemsForCartsDetails => ResourceKey("GetCartItemsForCartsDetails.g.sql");
        }
    }
}