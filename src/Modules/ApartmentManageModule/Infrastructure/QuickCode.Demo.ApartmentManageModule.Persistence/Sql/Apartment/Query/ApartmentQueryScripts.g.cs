namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Apartment
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.Apartment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetApartmentsBySite => ResourceKey("GetApartmentsBySite.g.sql");
            public static string GetActiveApartments => ResourceKey("GetActiveApartments.g.sql");
            public static string GetFlatsForApartments => ResourceKey("GetFlatsForApartments.g.sql");
            public static string GetFlatsForApartmentsDetails => ResourceKey("GetFlatsForApartmentsDetails.g.sql");
            public static string GetFlatPaymentsForApartments => ResourceKey("GetFlatPaymentsForApartments.g.sql");
            public static string GetFlatPaymentsForApartmentsDetails => ResourceKey("GetFlatPaymentsForApartmentsDetails.g.sql");
            public static string GetCommonExpensesForApartments => ResourceKey("GetCommonExpensesForApartments.g.sql");
            public static string GetCommonExpensesForApartmentsDetails => ResourceKey("GetCommonExpensesForApartmentsDetails.g.sql");
            public static string GetApartmentFeePlansForApartments => ResourceKey("GetApartmentFeePlansForApartments.g.sql");
            public static string GetApartmentFeePlansForApartmentsDetails => ResourceKey("GetApartmentFeePlansForApartmentsDetails.g.sql");
            public static string GetExpenseInstallmentsForApartments => ResourceKey("GetExpenseInstallmentsForApartments.g.sql");
            public static string GetExpenseInstallmentsForApartmentsDetails => ResourceKey("GetExpenseInstallmentsForApartmentsDetails.g.sql");
            public static string GetFlatExpenseInstallmentsForApartments => ResourceKey("GetFlatExpenseInstallmentsForApartments.g.sql");
            public static string GetFlatExpenseInstallmentsForApartmentsDetails => ResourceKey("GetFlatExpenseInstallmentsForApartmentsDetails.g.sql");
        }
    }
}