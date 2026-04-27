namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PayoutPeriod
    {
        public static class Query
        {
            private const string _prefix = "FinanceSettlementModule.PayoutPeriod.Query";
            private const string _sqlScriptStem = "PayoutPeriod";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetOpenPeriod => ResourceKey($"{_sqlScriptStem}.GetOpenPeriod.g.sql");
        }
    }
}