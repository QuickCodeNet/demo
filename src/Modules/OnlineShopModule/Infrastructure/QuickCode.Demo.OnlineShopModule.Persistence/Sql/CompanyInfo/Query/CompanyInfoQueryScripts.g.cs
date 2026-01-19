namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CompanyInfo
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.CompanyInfo.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetCompanyContact => ResourceKey("GetCompanyContact.g.sql");
        }
    }
}