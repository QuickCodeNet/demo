namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Shipment
    {
        public static class Command
        {
            private const string _prefix = "OrderManagementModule.Shipment.Command";
            private const string _sqlScriptStem = "Shipment";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string MarkAsShipped => ResourceKey($"{_sqlScriptStem}.MarkAsShipped.g.sql");
            public static string MarkAsDelivered => ResourceKey($"{_sqlScriptStem}.MarkAsDelivered.g.sql");
        }
    }
}