namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Seller
    {
        public static class Query
        {
            private const string _prefix = "SellerManagementModule.Seller.Query";
            private const string _sqlScriptStem = "Seller";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByUserId => ResourceKey($"{_sqlScriptStem}.GetByUserId.g.sql");
            public static string GetByStatus => ResourceKey($"{_sqlScriptStem}.GetByStatus.g.sql");
            public static string SearchByCompanyName => ResourceKey($"{_sqlScriptStem}.SearchByCompanyName.g.sql");
            public static string GetPendingVerificationCount => ResourceKey($"{_sqlScriptStem}.GetPendingVerificationCount.g.sql");
        }
    }
}