namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PaymentMonth
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.PaymentMonth.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetAllMonths => ResourceKey("GetAllMonths.g.sql");
            public static string GetApartmentFeePlansForPaymentMonths => ResourceKey("GetApartmentFeePlansForPaymentMonths.g.sql");
            public static string GetApartmentFeePlansForPaymentMonthsDetails => ResourceKey("GetApartmentFeePlansForPaymentMonthsDetails.g.sql");
            public static string GetFlatPaymentsForPaymentMonths => ResourceKey("GetFlatPaymentsForPaymentMonths.g.sql");
            public static string GetFlatPaymentsForPaymentMonthsDetails => ResourceKey("GetFlatPaymentsForPaymentMonthsDetails.g.sql");
            public static string GetCommonExpensesForPaymentMonths => ResourceKey("GetCommonExpensesForPaymentMonths.g.sql");
            public static string GetCommonExpensesForPaymentMonthsDetails => ResourceKey("GetCommonExpensesForPaymentMonthsDetails.g.sql");
        }
    }
}