namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class FlatContact
    {
        public static class Query
        {
            public const string GetFlatOwners = "ApartmentManageModule.FlatContact.Query.GetFlatOwners.g.sql";
            public const string GetFlatTenants = "ApartmentManageModule.FlatContact.Query.GetFlatTenants.g.sql";
            public const string GetContactFlats = "ApartmentManageModule.FlatContact.Query.GetContactFlats.g.sql";
            public const string GetContactOwnedFlats = "ApartmentManageModule.FlatContact.Query.GetContactOwnedFlats.g.sql";
            public const string GetContactRentedFlats = "ApartmentManageModule.FlatContact.Query.GetContactRentedFlats.g.sql";
            public const string CheckFlatHasOwner = "ApartmentManageModule.FlatContact.Query.CheckFlatHasOwner.g.sql";
            public const string CheckFlatHasTenant = "ApartmentManageModule.FlatContact.Query.CheckFlatHasTenant.g.sql";
            public const string GetFlatContactsCount = "ApartmentManageModule.FlatContact.Query.GetFlatContactsCount.g.sql";
        }
    }
}