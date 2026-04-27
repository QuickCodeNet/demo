namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerBankAccount
    {
        public static class Command
        {
            private const string _prefix = "SellerManagementModule.SellerBankAccount.Command";
            private const string _sqlScriptStem = "SellerBankAccount";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string SetAsDefault => ResourceKey($"{_sqlScriptStem}.SetAsDefault.g.sql");
            public static string SetDefaultAccount => ResourceKey($"{_sqlScriptStem}.SetDefaultAccount.g.sql");
        }
    }
}