namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CommonExpense
    {
        public static class Command
        {
            private const string _prefix = "ApartmentManageModule.CommonExpense.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string MarkExpenseAsPaid => ResourceKey("MarkExpenseAsPaid.g.sql");
        }
    }
}