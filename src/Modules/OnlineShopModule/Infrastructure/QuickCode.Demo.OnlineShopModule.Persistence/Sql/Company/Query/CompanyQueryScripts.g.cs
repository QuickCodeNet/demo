namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Company
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Company.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetCompanies => ResourceKey("GetCompanies.g.sql");
            public static string GetUsersForCompanies => ResourceKey("GetUsersForCompanies.g.sql");
            public static string GetUsersForCompaniesDetails => ResourceKey("GetUsersForCompaniesDetails.g.sql");
        }
    }
}