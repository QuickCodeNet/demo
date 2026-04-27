namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CommissionRule
    {
        public static class Command
        {
            private const string _prefix = "PricingEngineModule.CommissionRule.Command";
            private const string _sqlScriptStem = "CommissionRule";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Deactivate => ResourceKey($"{_sqlScriptStem}.Deactivate.g.sql");
        }
    }
}