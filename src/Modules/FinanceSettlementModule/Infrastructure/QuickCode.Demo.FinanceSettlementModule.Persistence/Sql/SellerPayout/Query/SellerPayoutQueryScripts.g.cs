namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerPayout
    {
        public static class Query
        {
            private const string _prefix = "FinanceSettlementModule.SellerPayout.Query";
            private const string _sqlScriptStem = "SellerPayout";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
            public static string GetByStatus => ResourceKey($"{_sqlScriptStem}.GetByStatus.g.sql");
            public static string GetByPeriod => ResourceKey($"{_sqlScriptStem}.GetByPeriod.g.sql");
            public static string GetPendingPayoutsSummary => ResourceKey($"{_sqlScriptStem}.GetPendingPayoutsSummary.g.sql");
        }
    }
}