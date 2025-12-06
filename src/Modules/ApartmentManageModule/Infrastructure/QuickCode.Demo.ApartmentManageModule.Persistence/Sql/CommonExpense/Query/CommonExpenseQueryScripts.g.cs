namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class CommonExpense
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.CommonExpense.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetExpensesByApartmentMonth => ResourceKey("GetExpensesByApartmentMonth.g.sql");
            public static string GetExpensesBySite => ResourceKey("GetExpensesBySite.g.sql");
            public static string GetExpensesSummaryByYear => ResourceKey("GetExpensesSummaryByYear.g.sql");
            public static string GetExpensesByType => ResourceKey("GetExpensesByType.g.sql");
            public static string GetUnpaidExpensesByApartment => ResourceKey("GetUnpaidExpensesByApartment.g.sql");
            public static string GetExpensesCountByApartment => ResourceKey("GetExpensesCountByApartment.g.sql");
            public static string GetTotalExpenseAmountByApartment => ResourceKey("GetTotalExpenseAmountByApartment.g.sql");
            public static string GetFlatPaymentsForCommonExpenses => ResourceKey("GetFlatPaymentsForCommonExpenses.g.sql");
            public static string GetFlatPaymentsForCommonExpensesDetails => ResourceKey("GetFlatPaymentsForCommonExpensesDetails.g.sql");
            public static string GetExpenseInstallmentsForCommonExpenses => ResourceKey("GetExpenseInstallmentsForCommonExpenses.g.sql");
            public static string GetExpenseInstallmentsForCommonExpensesDetails => ResourceKey("GetExpenseInstallmentsForCommonExpensesDetails.g.sql");
            public static string GetFlatExpenseInstallmentsForCommonExpenses => ResourceKey("GetFlatExpenseInstallmentsForCommonExpenses.g.sql");
            public static string GetFlatExpenseInstallmentsForCommonExpensesDetails => ResourceKey("GetFlatExpenseInstallmentsForCommonExpensesDetails.g.sql");
        }
    }
}