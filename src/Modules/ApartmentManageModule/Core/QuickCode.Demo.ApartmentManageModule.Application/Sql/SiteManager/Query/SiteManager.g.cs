namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class SiteManager
    {
        public static class Query
        {
            public const string GetSiteManagers = "ApartmentManageModule.SiteManager.Query.GetSiteManagers.g.sql";
            public const string GetActiveManagerBySite = "ApartmentManageModule.SiteManager.Query.GetActiveManagerBySite.g.sql";
            public const string GetSiteManagerWithContact = "ApartmentManageModule.SiteManager.Query.GetSiteManagerWithContact.g.sql";
            public const string CheckSiteHasManager = "ApartmentManageModule.SiteManager.Query.CheckSiteHasManager.g.sql";
        }
    }
}