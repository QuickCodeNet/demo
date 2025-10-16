namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class FeeType
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.FeeType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveFeeTypes => ResourceKey("GetActiveFeeTypes.g.sql");
            public static string GetFlatPaymentsForFeeTypes => ResourceKey("GetFlatPaymentsForFeeTypes.g.sql");
            public static string GetFlatPaymentsForFeeTypesDetails => ResourceKey("GetFlatPaymentsForFeeTypesDetails.g.sql");
        }
    }
}