namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CommissionEntry
    {
        public static class Query
        {
            private const string _prefix = "FinanceSettlementModule.CommissionEntry.Query";
            private const string _sqlScriptStem = "CommissionEntry";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByOrderId => ResourceKey($"{_sqlScriptStem}.GetByOrderId.g.sql");
            public static string GetCommissionsBySellerForPeriod => ResourceKey($"{_sqlScriptStem}.GetCommissionsBySellerForPeriod.g.sql");
        }
    }
}