namespace QuickCode.Demo.FinanceSettlementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PayoutPeriod
    {
        public static class Command
        {
            private const string _prefix = "FinanceSettlementModule.PayoutPeriod.Command";
            private const string _sqlScriptStem = "PayoutPeriod";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ClosePeriod => ResourceKey($"{_sqlScriptStem}.ClosePeriod.g.sql");
        }
    }
}