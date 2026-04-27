namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerCommissionAssignment
    {
        public static class Command
        {
            private const string _prefix = "PricingEngineModule.SellerCommissionAssignment.Command";
            private const string _sqlScriptStem = "SellerCommissionAssignment";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string RemoveAssignment => ResourceKey($"{_sqlScriptStem}.RemoveAssignment.g.sql");
        }
    }
}