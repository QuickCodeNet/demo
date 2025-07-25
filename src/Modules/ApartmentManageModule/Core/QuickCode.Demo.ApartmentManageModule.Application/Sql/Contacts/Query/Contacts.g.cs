namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class Contacts
    {
        public static class Query
        {
            public const string GetActiveContacts = "ApartmentManageModule.Contacts.Query.GetActiveContacts.g.sql";
            public const string GetContactById = "ApartmentManageModule.Contacts.Query.GetContactById.g.sql";
            public const string GetContactByPhone = "ApartmentManageModule.Contacts.Query.GetContactByPhone.g.sql";
            public const string GetContactByEmail = "ApartmentManageModule.Contacts.Query.GetContactByEmail.g.sql";
            public const string GetContactByIdentity = "ApartmentManageModule.Contacts.Query.GetContactByIdentity.g.sql";
            public const string CheckContactByPhone = "ApartmentManageModule.Contacts.Query.CheckContactByPhone.g.sql";
            public const string CheckContactByEmail = "ApartmentManageModule.Contacts.Query.CheckContactByEmail.g.sql";
            public const string GetActiveContactsCount = "ApartmentManageModule.Contacts.Query.GetActiveContactsCount.g.sql";
            public const string GetContactsWithPager = "ApartmentManageModule.Contacts.Query.GetContactsWithPager.g.sql";
            public const string GetFlatContactsForContacts = "ApartmentManageModule.Contacts.Query.GetFlatContactsForContacts.g.sql";
            public const string GetFlatContactsForContactsDetails = "ApartmentManageModule.Contacts.Query.GetFlatContactsForContactsDetails.g.sql";
            public const string GetSiteManagersForContacts = "ApartmentManageModule.Contacts.Query.GetSiteManagersForContacts.g.sql";
            public const string GetSiteManagersForContactsDetails = "ApartmentManageModule.Contacts.Query.GetSiteManagersForContactsDetails.g.sql";
        }
    }
}