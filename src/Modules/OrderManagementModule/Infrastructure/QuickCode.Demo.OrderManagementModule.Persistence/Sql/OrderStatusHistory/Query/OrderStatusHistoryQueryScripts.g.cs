namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OrderStatusHistory
    {
        public static class Query
        {
            private const string _prefix = "OrderManagementModule.OrderStatusHistory.Query";
            private const string _sqlScriptStem = "OrderStatusHistory";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByOrderId => ResourceKey($"{_sqlScriptStem}.GetByOrderId.g.sql");
        }
    }
}