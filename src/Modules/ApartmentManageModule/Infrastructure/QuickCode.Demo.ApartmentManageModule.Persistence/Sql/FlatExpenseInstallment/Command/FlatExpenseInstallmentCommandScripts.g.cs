namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class FlatExpenseInstallment
    {
        public static class Command
        {
            private const string _prefix = "ApartmentManageModule.FlatExpenseInstallment.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string MarkFlatInstallmentAsPaid => ResourceKey("MarkFlatInstallmentAsPaid.g.sql");
        }
    }
}