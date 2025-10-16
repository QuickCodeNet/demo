namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class FlatExpenseInstallment
    {
        public static class Query
        {
            public const string GetFlatExpenseInstallments = "ApartmentManageModule.FlatExpenseInstallment.Query.GetFlatExpenseInstallments.g.sql";
            public const string GetFlatUnpaidInstallments = "ApartmentManageModule.FlatExpenseInstallment.Query.GetFlatUnpaidInstallments.g.sql";
            public const string GetFlatOverdueInstallments = "ApartmentManageModule.FlatExpenseInstallment.Query.GetFlatOverdueInstallments.g.sql";
            public const string GetApartmentFlatInstallments = "ApartmentManageModule.FlatExpenseInstallment.Query.GetApartmentFlatInstallments.g.sql";
            public const string GetFlatTotalDebt = "ApartmentManageModule.FlatExpenseInstallment.Query.GetFlatTotalDebt.g.sql";
        }
    }
}