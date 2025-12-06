namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ApartmentFeePlan
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.ApartmentFeePlan.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetFeePlanByYearMonth => ResourceKey("GetFeePlanByYearMonth.g.sql");
            public static string GetFeePlansBySite => ResourceKey("GetFeePlansBySite.g.sql");
            public static string GetFlatPaymentsForApartmentFeePlans => ResourceKey("GetFlatPaymentsForApartmentFeePlans.g.sql");
            public static string GetFlatPaymentsForApartmentFeePlansDetails => ResourceKey("GetFlatPaymentsForApartmentFeePlansDetails.g.sql");
        }
    }
}