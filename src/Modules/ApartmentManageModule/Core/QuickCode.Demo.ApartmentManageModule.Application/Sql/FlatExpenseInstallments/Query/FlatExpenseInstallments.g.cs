namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class FlatExpenseInstallments
    {
        public static class Query
        {
            public const string GetFlatExpenseInstallments = "ApartmentManageModule.FlatExpenseInstallments.Query.GetFlatExpenseInstallments.g.sql";
            public const string GetFlatUnpaidInstallments = "ApartmentManageModule.FlatExpenseInstallments.Query.GetFlatUnpaidInstallments.g.sql";
            public const string GetFlatOverdueInstallments = "ApartmentManageModule.FlatExpenseInstallments.Query.GetFlatOverdueInstallments.g.sql";
            public const string GetApartmentFlatInstallments = "ApartmentManageModule.FlatExpenseInstallments.Query.GetApartmentFlatInstallments.g.sql";
            public const string GetFlatTotalDebt = "ApartmentManageModule.FlatExpenseInstallments.Query.GetFlatTotalDebt.g.sql";
        }
    }
}