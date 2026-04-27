namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerDocument
    {
        public static class Command
        {
            private const string _prefix = "SellerManagementModule.SellerDocument.Command";
            private const string _sqlScriptStem = "SellerDocument";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Verify => ResourceKey($"{_sqlScriptStem}.Verify.g.sql");
            public static string Reject => ResourceKey($"{_sqlScriptStem}.Reject.g.sql");
        }
    }
}