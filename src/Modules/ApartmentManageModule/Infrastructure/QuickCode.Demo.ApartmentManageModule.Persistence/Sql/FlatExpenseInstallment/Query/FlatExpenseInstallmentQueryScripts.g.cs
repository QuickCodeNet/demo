namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class FlatExpenseInstallment
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.FlatExpenseInstallment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetFlatExpenseInstallments => ResourceKey("GetFlatExpenseInstallments.g.sql");
            public static string GetFlatUnpaidInstallments => ResourceKey("GetFlatUnpaidInstallments.g.sql");
            public static string GetFlatOverdueInstallments => ResourceKey("GetFlatOverdueInstallments.g.sql");
            public static string GetApartmentFlatInstallments => ResourceKey("GetApartmentFlatInstallments.g.sql");
            public static string GetFlatTotalDebt => ResourceKey("GetFlatTotalDebt.g.sql");
        }
    }
}