namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Payment
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Payment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetPendingPayments => ResourceKey("GetPendingPayments.g.sql");
            public static string GetOrdersForPayments => ResourceKey("GetOrdersForPayments.g.sql");
            public static string GetOrdersForPaymentsDetails => ResourceKey("GetOrdersForPaymentsDetails.g.sql");
        }
    }
}