namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class BlackList
    {
        public static class Query
        {
            private const string _prefix = "EmailManagerModule.BlackList.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetById => ResourceKey("GetById.g.sql");
            public static string GetByRecipient => ResourceKey("GetByRecipient.g.sql");
            public static string ExistsByRecipient => ResourceKey("ExistsByRecipient.g.sql");
            public static string GetBlacklistCount => ResourceKey("GetBlacklistCount.g.sql");
        }
    }
}