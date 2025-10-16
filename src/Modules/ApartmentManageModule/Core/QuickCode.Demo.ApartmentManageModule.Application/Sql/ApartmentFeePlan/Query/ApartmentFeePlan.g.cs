namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class ApartmentFeePlan
    {
        public static class Query
        {
            public const string GetFeePlanByYearMonth = "ApartmentManageModule.ApartmentFeePlan.Query.GetFeePlanByYearMonth.g.sql";
            public const string GetFeePlansBySite = "ApartmentManageModule.ApartmentFeePlan.Query.GetFeePlansBySite.g.sql";
            public const string GetFlatPaymentsForApartmentFeePlans = "ApartmentManageModule.ApartmentFeePlan.Query.GetFlatPaymentsForApartmentFeePlans.g.sql";
            public const string GetFlatPaymentsForApartmentFeePlansDetails = "ApartmentManageModule.ApartmentFeePlan.Query.GetFlatPaymentsForApartmentFeePlansDetails.g.sql";
        }
    }
}