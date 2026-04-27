namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PayoutLineItem
    {
        public static class Query
        {
            private const string _prefix = "FinanceSettlementModule.PayoutLineItem.Query";
            private const string _sqlScriptStem = "PayoutLineItem";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByPayoutId => ResourceKey($"{_sqlScriptStem}.GetByPayoutId.g.sql");
        }
    }
}