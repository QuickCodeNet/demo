namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Order
    {
        public static class Query
        {
            private const string _prefix = "OrderManagementModule.Order.Query";
            private const string _sqlScriptStem = "Order";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByOrderNumber => ResourceKey($"{_sqlScriptStem}.GetByOrderNumber.g.sql");
            public static string GetByCustomerId => ResourceKey($"{_sqlScriptStem}.GetByCustomerId.g.sql");
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetByStatus => ResourceKey($"{_sqlScriptStem}.GetByStatus.g.sql");
            public static string GetByDateRange => ResourceKey($"{_sqlScriptStem}.GetByDateRange.g.sql");
            public static string GetOrdersForSettlement => ResourceKey($"{_sqlScriptStem}.GetOrdersForSettlement.g.sql");
            public static string GetMonthlyRevenueBySeller => ResourceKey($"{_sqlScriptStem}.GetMonthlyRevenueBySeller.g.sql");
        }
    }
}