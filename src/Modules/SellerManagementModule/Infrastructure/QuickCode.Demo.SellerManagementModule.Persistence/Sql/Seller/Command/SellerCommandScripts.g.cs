namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Seller
    {
        public static class Command
        {
            private const string _prefix = "SellerManagementModule.Seller.Command";
            private const string _sqlScriptStem = "Seller";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Approve => ResourceKey($"{_sqlScriptStem}.Approve.g.sql");
            public static string Suspend => ResourceKey($"{_sqlScriptStem}.Suspend.g.sql");
            public static string Reject => ResourceKey($"{_sqlScriptStem}.Reject.g.sql");
            public static string UpdateTier => ResourceKey($"{_sqlScriptStem}.UpdateTier.g.sql");
        }
    }
}