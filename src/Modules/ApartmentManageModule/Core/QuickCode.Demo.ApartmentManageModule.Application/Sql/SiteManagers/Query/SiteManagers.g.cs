namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class SiteManagers
    {
        public static class Query
        {
            public const string GetSiteManagers = "ApartmentManageModule.SiteManagers.Query.GetSiteManagers.g.sql";
            public const string GetActiveManagerBySite = "ApartmentManageModule.SiteManagers.Query.GetActiveManagerBySite.g.sql";
            public const string GetSiteManagerWithContact = "ApartmentManageModule.SiteManagers.Query.GetSiteManagerWithContact.g.sql";
            public const string CheckSiteHasManager = "ApartmentManageModule.SiteManagers.Query.CheckSiteHasManager.g.sql";
        }
    }
}