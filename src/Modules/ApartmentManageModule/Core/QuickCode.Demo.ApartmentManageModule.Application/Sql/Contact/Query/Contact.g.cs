namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class Contact
    {
        public static class Query
        {
            public const string GetActiveContacts = "ApartmentManageModule.Contact.Query.GetActiveContacts.g.sql";
            public const string GetContactById = "ApartmentManageModule.Contact.Query.GetContactById.g.sql";
            public const string GetContactByPhone = "ApartmentManageModule.Contact.Query.GetContactByPhone.g.sql";
            public const string GetContactByEmail = "ApartmentManageModule.Contact.Query.GetContactByEmail.g.sql";
            public const string GetContactByIdentity = "ApartmentManageModule.Contact.Query.GetContactByIdentity.g.sql";
            public const string CheckContactByPhone = "ApartmentManageModule.Contact.Query.CheckContactByPhone.g.sql";
            public const string CheckContactByEmail = "ApartmentManageModule.Contact.Query.CheckContactByEmail.g.sql";
            public const string GetActiveContactsCount = "ApartmentManageModule.Contact.Query.GetActiveContactsCount.g.sql";
            public const string GetContactsWithPager = "ApartmentManageModule.Contact.Query.GetContactsWithPager.g.sql";
            public const string GetFlatContactsForContacts = "ApartmentManageModule.Contact.Query.GetFlatContactsForContacts.g.sql";
            public const string GetFlatContactsForContactsDetails = "ApartmentManageModule.Contact.Query.GetFlatContactsForContactsDetails.g.sql";
            public const string GetSiteManagersForContacts = "ApartmentManageModule.Contact.Query.GetSiteManagersForContacts.g.sql";
            public const string GetSiteManagersForContactsDetails = "ApartmentManageModule.Contact.Query.GetSiteManagersForContactsDetails.g.sql";
        }
    }
}