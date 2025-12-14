namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Shipment
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Shipment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetUndeliveredShipments => ResourceKey("GetUndeliveredShipments.g.sql");
            public static string TrackShipment => ResourceKey("TrackShipment.g.sql");
        }
    }
}