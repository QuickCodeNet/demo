namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseInstallment
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.ExpenseInstallment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetExpenseInstallments => ResourceKey("GetExpenseInstallments.g.sql");
            public static string GetUnpaidInstallments => ResourceKey("GetUnpaidInstallments.g.sql");
            public static string GetOverdueInstallments => ResourceKey("GetOverdueInstallments.g.sql");
            public static string GetApartmentInstallments => ResourceKey("GetApartmentInstallments.g.sql");
            public static string GetSiteInstallments => ResourceKey("GetSiteInstallments.g.sql");
            public static string GetFlatExpenseInstallmentsForExpenseInstallments => ResourceKey("GetFlatExpenseInstallmentsForExpenseInstallments.g.sql");
            public static string GetFlatExpenseInstallmentsForExpenseInstallmentsDetails => ResourceKey("GetFlatExpenseInstallmentsForExpenseInstallmentsDetails.g.sql");
        }
    }
}