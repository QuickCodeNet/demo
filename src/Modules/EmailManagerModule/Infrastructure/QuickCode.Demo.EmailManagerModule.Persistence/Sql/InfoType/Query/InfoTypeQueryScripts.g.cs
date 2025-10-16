namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class InfoType
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.InfoType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetInfoMessagesForInfoTypes => ResourceKey("GetInfoMessagesForInfoTypes.g.sql");
            public static string GetInfoMessagesForInfoTypesDetails => ResourceKey("GetInfoMessagesForInfoTypesDetails.g.sql");
        }
    }
}