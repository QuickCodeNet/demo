namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Cart
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.Cart.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string DeactivateStaleCarts => ResourceKey("DeactivateStaleCarts.g.sql");
        }
    }
}