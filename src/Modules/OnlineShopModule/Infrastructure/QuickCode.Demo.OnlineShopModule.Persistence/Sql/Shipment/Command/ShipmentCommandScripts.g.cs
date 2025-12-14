namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Shipment
    {
        public static class Command
        {
            private const string _prefix = "OnlineShopModule.Shipment.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string MarkShipmentDelivered => ResourceKey("MarkShipmentDelivered.g.sql");
        }
    }
}