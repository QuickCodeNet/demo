namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class ApartmentFeePlans
    {
        public static class Query
        {
            public const string GetFeePlanByYearMonth = "ApartmentManageModule.ApartmentFeePlans.Query.GetFeePlanByYearMonth.g.sql";
            public const string GetFeePlansBySite = "ApartmentManageModule.ApartmentFeePlans.Query.GetFeePlansBySite.g.sql";
            public const string GetFlatPaymentsForApartmentFeePlans = "ApartmentManageModule.ApartmentFeePlans.Query.GetFlatPaymentsForApartmentFeePlans.g.sql";
            public const string GetFlatPaymentsForApartmentFeePlansDetails = "ApartmentManageModule.ApartmentFeePlans.Query.GetFlatPaymentsForApartmentFeePlansDetails.g.sql";
        }
    }
}