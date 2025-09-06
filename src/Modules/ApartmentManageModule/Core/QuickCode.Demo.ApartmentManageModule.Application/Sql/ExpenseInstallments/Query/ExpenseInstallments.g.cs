namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseInstallments
    {
        public static class Query
        {
            public const string GetExpenseInstallments = "ApartmentManageModule.ExpenseInstallments.Query.GetExpenseInstallments.g.sql";
            public const string GetUnpaidInstallments = "ApartmentManageModule.ExpenseInstallments.Query.GetUnpaidInstallments.g.sql";
            public const string GetOverdueInstallments = "ApartmentManageModule.ExpenseInstallments.Query.GetOverdueInstallments.g.sql";
            public const string GetApartmentInstallments = "ApartmentManageModule.ExpenseInstallments.Query.GetApartmentInstallments.g.sql";
            public const string GetSiteInstallments = "ApartmentManageModule.ExpenseInstallments.Query.GetSiteInstallments.g.sql";
            public const string GetFlatExpenseInstallmentsForExpenseInstallments = "ApartmentManageModule.ExpenseInstallments.Query.GetFlatExpenseInstallmentsForExpenseInstallments.g.sql";
            public const string GetFlatExpenseInstallmentsForExpenseInstallmentsDetails = "ApartmentManageModule.ExpenseInstallments.Query.GetFlatExpenseInstallmentsForExpenseInstallmentsDetails.g.sql";
        }
    }
}