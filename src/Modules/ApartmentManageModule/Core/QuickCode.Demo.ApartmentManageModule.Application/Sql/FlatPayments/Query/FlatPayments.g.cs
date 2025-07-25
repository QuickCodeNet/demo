namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class FlatPayments
    {
        public static class Query
        {
            public const string GetPaymentsByFlatYearMonth = "ApartmentManageModule.FlatPayments.Query.GetPaymentsByFlatYearMonth.g.sql";
            public const string GetUnpaidPaymentsByFlat = "ApartmentManageModule.FlatPayments.Query.GetUnpaidPaymentsByFlat.g.sql";
            public const string GetUnpaidPaymentsBySite = "ApartmentManageModule.FlatPayments.Query.GetUnpaidPaymentsBySite.g.sql";
            public const string GetTotalCashInSafe = "ApartmentManageModule.FlatPayments.Query.GetTotalCashInSafe.g.sql";
            public const string GetPendingPaymentsByFlatYearMonth = "ApartmentManageModule.FlatPayments.Query.GetPendingPaymentsByFlatYearMonth.g.sql";
            public const string GetFlatPaymentsByMonth = "ApartmentManageModule.FlatPayments.Query.GetFlatPaymentsByMonth.g.sql";
            public const string GetPaymentsCountByFlat = "ApartmentManageModule.FlatPayments.Query.GetPaymentsCountByFlat.g.sql";
            public const string GetTotalPaidAmountByFlat = "ApartmentManageModule.FlatPayments.Query.GetTotalPaidAmountByFlat.g.sql";
            public const string GetUnpaidPaymentsCountBySite = "ApartmentManageModule.FlatPayments.Query.GetUnpaidPaymentsCountBySite.g.sql";
        }
    }
}