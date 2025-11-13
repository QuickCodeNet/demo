namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Site
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.Site.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveSites => ResourceKey("GetActiveSites.g.sql");
            public static string GetSiteById => ResourceKey("GetSiteById.g.sql");
            public static string GetFlatsCountBySite => ResourceKey("GetFlatsCountBySite.g.sql");
            public static string GetOwnersCountBySite => ResourceKey("GetOwnersCountBySite.g.sql");
            public static string GetTenantsCountBySite => ResourceKey("GetTenantsCountBySite.g.sql");
            public static string GetTotalPaymentsBySite => ResourceKey("GetTotalPaymentsBySite.g.sql");
            public static string GetApartmentsForSites => ResourceKey("GetApartmentsForSites.g.sql");
            public static string GetApartmentsForSitesDetails => ResourceKey("GetApartmentsForSitesDetails.g.sql");
            public static string GetFlatsForSites => ResourceKey("GetFlatsForSites.g.sql");
            public static string GetFlatsForSitesDetails => ResourceKey("GetFlatsForSitesDetails.g.sql");
            public static string GetSiteManagersForSites => ResourceKey("GetSiteManagersForSites.g.sql");
            public static string GetSiteManagersForSitesDetails => ResourceKey("GetSiteManagersForSitesDetails.g.sql");
            public static string GetFlatPaymentsForSites => ResourceKey("GetFlatPaymentsForSites.g.sql");
            public static string GetFlatPaymentsForSitesDetails => ResourceKey("GetFlatPaymentsForSitesDetails.g.sql");
            public static string GetCommonExpensesForSites => ResourceKey("GetCommonExpensesForSites.g.sql");
            public static string GetCommonExpensesForSitesDetails => ResourceKey("GetCommonExpensesForSitesDetails.g.sql");
            public static string GetApartmentFeePlansForSites => ResourceKey("GetApartmentFeePlansForSites.g.sql");
            public static string GetApartmentFeePlansForSitesDetails => ResourceKey("GetApartmentFeePlansForSitesDetails.g.sql");
            public static string GetExpenseInstallmentsForSites => ResourceKey("GetExpenseInstallmentsForSites.g.sql");
            public static string GetExpenseInstallmentsForSitesDetails => ResourceKey("GetExpenseInstallmentsForSitesDetails.g.sql");
            public static string GetFlatExpenseInstallmentsForSites => ResourceKey("GetFlatExpenseInstallmentsForSites.g.sql");
            public static string GetFlatExpenseInstallmentsForSitesDetails => ResourceKey("GetFlatExpenseInstallmentsForSitesDetails.g.sql");
            public static string GetUserSiteAccessesForSites => ResourceKey("GetUserSiteAccessesForSites.g.sql");
            public static string GetUserSiteAccessesForSitesDetails => ResourceKey("GetUserSiteAccessesForSitesDetails.g.sql");
        }
    }
}