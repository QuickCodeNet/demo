namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Contact
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.Contact.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveContacts => ResourceKey("GetActiveContacts.g.sql");
            public static string GetContactById => ResourceKey("GetContactById.g.sql");
            public static string GetContactByPhone => ResourceKey("GetContactByPhone.g.sql");
            public static string GetContactByEmail => ResourceKey("GetContactByEmail.g.sql");
            public static string GetContactByIdentity => ResourceKey("GetContactByIdentity.g.sql");
            public static string CheckContactByPhone => ResourceKey("CheckContactByPhone.g.sql");
            public static string CheckContactByEmail => ResourceKey("CheckContactByEmail.g.sql");
            public static string GetActiveContactsCount => ResourceKey("GetActiveContactsCount.g.sql");
            public static string GetContactsWithPager => ResourceKey("GetContactsWithPager.g.sql");
            public static string GetFlatContactsForContacts => ResourceKey("GetFlatContactsForContacts.g.sql");
            public static string GetFlatContactsForContactsDetails => ResourceKey("GetFlatContactsForContactsDetails.g.sql");
            public static string GetSiteManagersForContacts => ResourceKey("GetSiteManagersForContacts.g.sql");
            public static string GetSiteManagersForContactsDetails => ResourceKey("GetSiteManagersForContactsDetails.g.sql");
        }
    }
}