namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class TransactionLedger
    {
        public static class Query
        {
            private const string _prefix = "FinanceSettlementModule.TransactionLedger.Query";
            private const string _sqlScriptStem = "TransactionLedger";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetSellerBalance => ResourceKey($"{_sqlScriptStem}.GetSellerBalance.g.sql");
            public static string GetTransactionsByTypeAndDate => ResourceKey($"{_sqlScriptStem}.GetTransactionsByTypeAndDate.g.sql");
        }
    }
}