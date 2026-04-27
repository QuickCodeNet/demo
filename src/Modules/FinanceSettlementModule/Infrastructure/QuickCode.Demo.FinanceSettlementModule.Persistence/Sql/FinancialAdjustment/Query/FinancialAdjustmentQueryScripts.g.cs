namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class FinancialAdjustment
    {
        public static class Query
        {
            private const string _prefix = "FinanceSettlementModule.FinancialAdjustment.Query";
            private const string _sqlScriptStem = "FinancialAdjustment";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetBySellerId => ResourceKey($"{_sqlScriptStem}.GetBySellerId.g.sql");
        }
    }
}