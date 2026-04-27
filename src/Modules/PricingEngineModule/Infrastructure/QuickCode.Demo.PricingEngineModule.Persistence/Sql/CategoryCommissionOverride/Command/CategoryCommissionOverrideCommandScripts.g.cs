namespace QuickCode.Demo.PricingEngineModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CategoryCommissionOverride
    {
        public static class Command
        {
            private const string _prefix = "PricingEngineModule.CategoryCommissionOverride.Command";
            private const string _sqlScriptStem = "CategoryCommissionOverride";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string RemoveOverride => ResourceKey($"{_sqlScriptStem}.RemoveOverride.g.sql");
        }
    }
}