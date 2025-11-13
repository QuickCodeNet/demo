namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Flat
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.Flat.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetFlatsByApartment => ResourceKey("GetFlatsByApartment.g.sql");
            public static string GetFlatsBySite => ResourceKey("GetFlatsBySite.g.sql");
            public static string GetFlatsWithContacts => ResourceKey("GetFlatsWithContacts.g.sql");
            public static string GetVacantFlats => ResourceKey("GetVacantFlats.g.sql");
            public static string GetRentedFlats => ResourceKey("GetRentedFlats.g.sql");
            public static string GetFlatByNumber => ResourceKey("GetFlatByNumber.g.sql");
            public static string GetOwnedFlats => ResourceKey("GetOwnedFlats.g.sql");
            public static string GetFlatsCountBySite => ResourceKey("GetFlatsCountBySite.g.sql");
            public static string GetFlatsCountByApartment => ResourceKey("GetFlatsCountByApartment.g.sql");
            public static string GetFlatContactsForFlats => ResourceKey("GetFlatContactsForFlats.g.sql");
            public static string GetFlatContactsForFlatsDetails => ResourceKey("GetFlatContactsForFlatsDetails.g.sql");
            public static string GetFlatPaymentsForFlats => ResourceKey("GetFlatPaymentsForFlats.g.sql");
            public static string GetFlatPaymentsForFlatsDetails => ResourceKey("GetFlatPaymentsForFlatsDetails.g.sql");
            public static string GetFlatExpenseInstallmentsForFlats => ResourceKey("GetFlatExpenseInstallmentsForFlats.g.sql");
            public static string GetFlatExpenseInstallmentsForFlatsDetails => ResourceKey("GetFlatExpenseInstallmentsForFlatsDetails.g.sql");
            public static string GetUserSiteAccessesForFlats => ResourceKey("GetUserSiteAccessesForFlats.g.sql");
            public static string GetUserSiteAccessesForFlatsDetails => ResourceKey("GetUserSiteAccessesForFlatsDetails.g.sql");
        }
    }
}