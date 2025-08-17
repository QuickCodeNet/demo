namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class FlatContacts
    {
        public static class Query
        {
            public const string GetFlatOwners = "ApartmentManageModule.FlatContacts.Query.GetFlatOwners.g.sql";
            public const string GetFlatTenants = "ApartmentManageModule.FlatContacts.Query.GetFlatTenants.g.sql";
            public const string GetContactFlats = "ApartmentManageModule.FlatContacts.Query.GetContactFlats.g.sql";
            public const string GetContactOwnedFlats = "ApartmentManageModule.FlatContacts.Query.GetContactOwnedFlats.g.sql";
            public const string GetContactRentedFlats = "ApartmentManageModule.FlatContacts.Query.GetContactRentedFlats.g.sql";
            public const string CheckFlatHasOwner = "ApartmentManageModule.FlatContacts.Query.CheckFlatHasOwner.g.sql";
            public const string CheckFlatHasTenant = "ApartmentManageModule.FlatContacts.Query.CheckFlatHasTenant.g.sql";
            public const string GetFlatContactsCount = "ApartmentManageModule.FlatContacts.Query.GetFlatContactsCount.g.sql";
        }
    }
}