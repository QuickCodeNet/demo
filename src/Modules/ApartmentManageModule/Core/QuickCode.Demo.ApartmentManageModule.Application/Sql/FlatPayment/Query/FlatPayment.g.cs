namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class FlatPayment
    {
        public static class Query
        {
            public const string GetPaymentsByFlatYearMonth = "ApartmentManageModule.FlatPayment.Query.GetPaymentsByFlatYearMonth.g.sql";
            public const string GetUnpaidPaymentsByFlat = "ApartmentManageModule.FlatPayment.Query.GetUnpaidPaymentsByFlat.g.sql";
            public const string GetUnpaidPaymentsBySite = "ApartmentManageModule.FlatPayment.Query.GetUnpaidPaymentsBySite.g.sql";
            public const string GetTotalCashInSafe = "ApartmentManageModule.FlatPayment.Query.GetTotalCashInSafe.g.sql";
            public const string GetPendingPaymentsByFlatYearMonth = "ApartmentManageModule.FlatPayment.Query.GetPendingPaymentsByFlatYearMonth.g.sql";
            public const string GetFlatPaymentsByMonth = "ApartmentManageModule.FlatPayment.Query.GetFlatPaymentsByMonth.g.sql";
            public const string GetPaymentsCountByFlat = "ApartmentManageModule.FlatPayment.Query.GetPaymentsCountByFlat.g.sql";
            public const string GetTotalPaidAmountByFlat = "ApartmentManageModule.FlatPayment.Query.GetTotalPaidAmountByFlat.g.sql";
            public const string GetUnpaidPaymentsCountBySite = "ApartmentManageModule.FlatPayment.Query.GetUnpaidPaymentsCountBySite.g.sql";
        }
    }
}