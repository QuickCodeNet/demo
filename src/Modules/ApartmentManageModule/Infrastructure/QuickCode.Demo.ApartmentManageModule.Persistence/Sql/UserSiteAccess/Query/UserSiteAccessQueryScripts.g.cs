namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class UserSiteAccess
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.UserSiteAccess.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetUserSites => ResourceKey("GetUserSites.g.sql");
            public static string GetSiteUsers => ResourceKey("GetSiteUsers.g.sql");
            public static string GetUserFlats => ResourceKey("GetUserFlats.g.sql");
        }
    }
}