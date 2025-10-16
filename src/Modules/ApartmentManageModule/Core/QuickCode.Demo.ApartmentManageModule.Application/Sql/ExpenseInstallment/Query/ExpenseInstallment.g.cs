namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseInstallment
    {
        public static class Query
        {
            public const string GetExpenseInstallments = "ApartmentManageModule.ExpenseInstallment.Query.GetExpenseInstallments.g.sql";
            public const string GetUnpaidInstallments = "ApartmentManageModule.ExpenseInstallment.Query.GetUnpaidInstallments.g.sql";
            public const string GetOverdueInstallments = "ApartmentManageModule.ExpenseInstallment.Query.GetOverdueInstallments.g.sql";
            public const string GetApartmentInstallments = "ApartmentManageModule.ExpenseInstallment.Query.GetApartmentInstallments.g.sql";
            public const string GetSiteInstallments = "ApartmentManageModule.ExpenseInstallment.Query.GetSiteInstallments.g.sql";
            public const string GetFlatExpenseInstallmentsForExpenseInstallments = "ApartmentManageModule.ExpenseInstallment.Query.GetFlatExpenseInstallmentsForExpenseInstallments.g.sql";
            public const string GetFlatExpenseInstallmentsForExpenseInstallmentsDetails = "ApartmentManageModule.ExpenseInstallment.Query.GetFlatExpenseInstallmentsForExpenseInstallmentsDetails.g.sql";
        }
    }
}