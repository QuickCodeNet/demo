namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CartItem
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.CartItem.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetCartItemsWithTotal => ResourceKey("GetCartItemsWithTotal.g.sql");
        }
    }
}