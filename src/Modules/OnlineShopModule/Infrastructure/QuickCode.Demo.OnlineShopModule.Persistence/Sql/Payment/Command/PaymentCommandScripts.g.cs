namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Payment
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.Payment.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string MarkPaymentFailed => ResourceKey("MarkPaymentFailed.g.sql");
        }
    }
}