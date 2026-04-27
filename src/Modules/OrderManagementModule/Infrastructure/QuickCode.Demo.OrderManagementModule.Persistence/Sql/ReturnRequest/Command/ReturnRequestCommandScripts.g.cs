namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ReturnRequest
    {
        public static class Command
        {
            private const string _prefix = "OrderManagementModule.ReturnRequest.Command";
            private const string _sqlScriptStem = "ReturnRequest";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Approve => ResourceKey($"{_sqlScriptStem}.Approve.g.sql");
            public static string Reject => ResourceKey($"{_sqlScriptStem}.Reject.g.sql");
            public static string Complete => ResourceKey($"{_sqlScriptStem}.Complete.g.sql");
        }
    }
}