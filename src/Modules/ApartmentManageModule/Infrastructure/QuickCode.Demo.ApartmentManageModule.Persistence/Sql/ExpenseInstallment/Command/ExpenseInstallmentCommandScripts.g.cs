namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseInstallment
    {
        public static class Command
        {
            private const string _prefix = "ApartmentManageModule.ExpenseInstallment.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string MarkInstallmentAsPaid => ResourceKey("MarkInstallmentAsPaid.g.sql");
        }
    }
}