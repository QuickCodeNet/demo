namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class HomePage
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.HomePage.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetHomePageContent => ResourceKey("GetHomePageContent.g.sql");
            public static string GetHomePageContentGr => ResourceKey("GetHomePageContentGr.g.sql");
        }
    }
}