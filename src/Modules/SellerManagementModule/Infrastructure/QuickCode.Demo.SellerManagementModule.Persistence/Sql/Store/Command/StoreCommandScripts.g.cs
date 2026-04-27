namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Store
    {
        public static class Command
        {
            private const string _prefix = "SellerManagementModule.Store.Command";
            private const string _sqlScriptStem = "Store";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateProfile => ResourceKey($"{_sqlScriptStem}.UpdateProfile.g.sql");
            public static string Deactivate => ResourceKey($"{_sqlScriptStem}.Deactivate.g.sql");
        }
    }
}