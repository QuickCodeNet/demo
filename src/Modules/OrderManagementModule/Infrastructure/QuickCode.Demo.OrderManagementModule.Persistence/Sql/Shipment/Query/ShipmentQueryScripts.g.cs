namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Shipment
    {
        public static class Query
        {
            private const string _prefix = "OrderManagementModule.Shipment.Query";
            private const string _sqlScriptStem = "Shipment";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByOrderId => ResourceKey($"{_sqlScriptStem}.GetByOrderId.g.sql");
            public static string GetByTrackingNumber => ResourceKey($"{_sqlScriptStem}.GetByTrackingNumber.g.sql");
        }
    }
}