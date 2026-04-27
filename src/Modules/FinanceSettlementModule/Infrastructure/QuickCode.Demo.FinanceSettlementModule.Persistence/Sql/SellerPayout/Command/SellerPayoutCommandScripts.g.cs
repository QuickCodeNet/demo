namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerPayout
    {
        public static class Command
        {
            private const string _prefix = "FinanceSettlementModule.SellerPayout.Command";
            private const string _sqlScriptStem = "SellerPayout";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Approve => ResourceKey($"{_sqlScriptStem}.Approve.g.sql");
            public static string MarkAsPaid => ResourceKey($"{_sqlScriptStem}.MarkAsPaid.g.sql");
            public static string MarkAsFailed => ResourceKey($"{_sqlScriptStem}.MarkAsFailed.g.sql");
        }
    }
}