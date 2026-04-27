namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Order
    {
        public static class Command
        {
            private const string _prefix = "OrderManagementModule.Order.Command";
            private const string _sqlScriptStem = "Order";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateStatus => ResourceKey($"{_sqlScriptStem}.UpdateStatus.g.sql");
            public static string CancelOrder => ResourceKey($"{_sqlScriptStem}.CancelOrder.g.sql");
        }
    }
}