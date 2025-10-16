namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class FlatPayment
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.FlatPayment.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetPaymentsByFlatYearMonth => ResourceKey("GetPaymentsByFlatYearMonth.g.sql");
            public static string GetUnpaidPaymentsByFlat => ResourceKey("GetUnpaidPaymentsByFlat.g.sql");
            public static string GetUnpaidPaymentsBySite => ResourceKey("GetUnpaidPaymentsBySite.g.sql");
            public static string GetTotalCashInSafe => ResourceKey("GetTotalCashInSafe.g.sql");
            public static string GetPendingPaymentsByFlatYearMonth => ResourceKey("GetPendingPaymentsByFlatYearMonth.g.sql");
            public static string GetFlatPaymentsByMonth => ResourceKey("GetFlatPaymentsByMonth.g.sql");
            public static string GetPaymentsCountByFlat => ResourceKey("GetPaymentsCountByFlat.g.sql");
            public static string GetTotalPaidAmountByFlat => ResourceKey("GetTotalPaidAmountByFlat.g.sql");
            public static string GetUnpaidPaymentsCountBySite => ResourceKey("GetUnpaidPaymentsCountBySite.g.sql");
        }
    }
}