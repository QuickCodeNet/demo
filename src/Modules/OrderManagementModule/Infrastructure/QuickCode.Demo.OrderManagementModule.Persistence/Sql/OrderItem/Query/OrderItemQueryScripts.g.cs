namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OrderItem
    {
        public static class Query
        {
            private const string _prefix = "OrderManagementModule.OrderItem.Query";
            private const string _sqlScriptStem = "OrderItem";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByOrderId => ResourceKey($"{_sqlScriptStem}.GetByOrderId.g.sql");
        }
    }
}