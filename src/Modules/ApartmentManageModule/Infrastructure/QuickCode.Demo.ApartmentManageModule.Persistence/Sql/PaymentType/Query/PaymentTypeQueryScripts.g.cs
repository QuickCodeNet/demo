namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PaymentType
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.PaymentType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActivePaymentTypes => ResourceKey("GetActivePaymentTypes.g.sql");
            public static string GetFlatPaymentsForPaymentTypes => ResourceKey("GetFlatPaymentsForPaymentTypes.g.sql");
            public static string GetFlatPaymentsForPaymentTypesDetails => ResourceKey("GetFlatPaymentsForPaymentTypesDetails.g.sql");
            public static string GetCommonExpensesForPaymentTypes => ResourceKey("GetCommonExpensesForPaymentTypes.g.sql");
            public static string GetCommonExpensesForPaymentTypesDetails => ResourceKey("GetCommonExpensesForPaymentTypesDetails.g.sql");
            public static string GetExpenseInstallmentsForPaymentTypes => ResourceKey("GetExpenseInstallmentsForPaymentTypes.g.sql");
            public static string GetExpenseInstallmentsForPaymentTypesDetails => ResourceKey("GetExpenseInstallmentsForPaymentTypesDetails.g.sql");
            public static string GetFlatExpenseInstallmentsForPaymentTypes => ResourceKey("GetFlatExpenseInstallmentsForPaymentTypes.g.sql");
            public static string GetFlatExpenseInstallmentsForPaymentTypesDetails => ResourceKey("GetFlatExpenseInstallmentsForPaymentTypesDetails.g.sql");
        }
    }
}