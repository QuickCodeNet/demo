namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ShippingMethod
    {
        public static class Command
        {
            private const string _prefix = "OrderManagementModule.ShippingMethod.Command";
            private const string _sqlScriptStem = "ShippingMethod";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Deactivate => ResourceKey($"{_sqlScriptStem}.Deactivate.g.sql");
        }
    }
}