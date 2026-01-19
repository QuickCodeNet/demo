namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class BlackList
    {
        public static class Command
        {
            private const string _prefix = "SmsManagerModule.BlackList.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string AddToBlacklist => ResourceKey("AddToBlacklist.g.sql");
            public static string RemoveFromSmsBlacklist => ResourceKey("RemoveFromSmsBlacklist.g.sql");
        }
    }
}