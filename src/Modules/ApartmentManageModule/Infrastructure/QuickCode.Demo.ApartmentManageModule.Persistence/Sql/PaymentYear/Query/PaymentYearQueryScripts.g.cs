namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PaymentYear
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.PaymentYear.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetAllYears => ResourceKey("GetAllYears.g.sql");
            public static string GetApartmentFeePlansForPaymentYears => ResourceKey("GetApartmentFeePlansForPaymentYears.g.sql");
            public static string GetApartmentFeePlansForPaymentYearsDetails => ResourceKey("GetApartmentFeePlansForPaymentYearsDetails.g.sql");
            public static string GetFlatPaymentsForPaymentYears => ResourceKey("GetFlatPaymentsForPaymentYears.g.sql");
            public static string GetFlatPaymentsForPaymentYearsDetails => ResourceKey("GetFlatPaymentsForPaymentYearsDetails.g.sql");
            public static string GetCommonExpensesForPaymentYears => ResourceKey("GetCommonExpensesForPaymentYears.g.sql");
            public static string GetCommonExpensesForPaymentYearsDetails => ResourceKey("GetCommonExpensesForPaymentYearsDetails.g.sql");
        }
    }
}