namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ReturnRequest
    {
        public static class Query
        {
            private const string _prefix = "OrderManagementModule.ReturnRequest.Query";
            private const string _sqlScriptStem = "ReturnRequest";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetByOrderId => ResourceKey($"{_sqlScriptStem}.GetByOrderId.g.sql");
            public static string GetPendingReturnsBySeller => ResourceKey($"{_sqlScriptStem}.GetPendingReturnsBySeller.g.sql");
        }
    }
}