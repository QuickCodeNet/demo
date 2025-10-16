namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SiteManager
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.SiteManager.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetSiteManagers => ResourceKey("GetSiteManagers.g.sql");
            public static string GetActiveManagerBySite => ResourceKey("GetActiveManagerBySite.g.sql");
            public static string GetSiteManagerWithContact => ResourceKey("GetSiteManagerWithContact.g.sql");
            public static string CheckSiteHasManager => ResourceKey("CheckSiteHasManager.g.sql");
        }
    }
}