namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerBankAccount
    {
        public static class Query
        {
            private const string _prefix = "SellerManagementModule.SellerBankAccount.Query";
            private const string _sqlScriptStem = "SellerBankAccount";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetDefaultBySellerId => ResourceKey($"{_sqlScriptStem}.GetDefaultBySellerId.g.sql");
        }
    }
}