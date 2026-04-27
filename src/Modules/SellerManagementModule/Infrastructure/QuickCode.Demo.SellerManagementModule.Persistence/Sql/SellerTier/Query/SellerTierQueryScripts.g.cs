namespace QuickCode.Demo.SellerManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class SellerTier
    {
        public static class Query
        {
            private const string _prefix = "SellerManagementModule.SellerTier.Query";
            private const string _sqlScriptStem = "SellerTier";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByName => ResourceKey($"{_sqlScriptStem}.GetByName.g.sql");
        }
    }
}