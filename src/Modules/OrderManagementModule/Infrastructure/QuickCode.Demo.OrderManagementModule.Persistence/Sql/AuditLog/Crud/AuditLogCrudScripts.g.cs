namespace QuickCode.Demo.OrderManagementModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class AuditLog
    {
        public static class Crud
        {
            private const string _prefix = "OrderManagementModule.AuditLog.Crud";
            private const string _sqlScriptStem = "AuditLog";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string Insert => ResourceKey($"{_sqlScriptStem}.Insert.g.sql");
            public static string Update => ResourceKey($"{_sqlScriptStem}.Update.g.sql");
            public static string Delete => ResourceKey($"{_sqlScriptStem}.Delete.g.sql");
            public static string GetByPk => ResourceKey($"{_sqlScriptStem}.GetByPk.g.sql");
            public static string List => ResourceKey($"{_sqlScriptStem}.List.g.sql");
            public static string Count => ResourceKey($"{_sqlScriptStem}.Count.g.sql");
        }
    }
}