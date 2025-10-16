namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class FlatContact
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.FlatContact.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetFlatOwners => ResourceKey("GetFlatOwners.g.sql");
            public static string GetFlatTenants => ResourceKey("GetFlatTenants.g.sql");
            public static string GetContactFlats => ResourceKey("GetContactFlats.g.sql");
            public static string GetContactOwnedFlats => ResourceKey("GetContactOwnedFlats.g.sql");
            public static string GetContactRentedFlats => ResourceKey("GetContactRentedFlats.g.sql");
            public static string CheckFlatHasOwner => ResourceKey("CheckFlatHasOwner.g.sql");
            public static string CheckFlatHasTenant => ResourceKey("CheckFlatHasTenant.g.sql");
            public static string GetFlatContactsCount => ResourceKey("GetFlatContactsCount.g.sql");
        }
    }
}