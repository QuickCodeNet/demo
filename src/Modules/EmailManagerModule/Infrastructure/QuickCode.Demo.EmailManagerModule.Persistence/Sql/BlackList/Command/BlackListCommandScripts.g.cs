namespace QuickCode.Demo.EmailManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class BlackList
    {
        public static class Command
        {
            private const string _prefix = "EmailManagerModule.BlackList.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string AddToBlacklist => ResourceKey("AddToBlacklist.g.sql");
            public static string RemoveFromBlacklist => ResourceKey("RemoveFromBlacklist.g.sql");
        }
    }
}