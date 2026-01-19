namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Product
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.Product.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ReduceStock => ResourceKey("ReduceStock.g.sql");
        }
    }
}