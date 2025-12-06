namespace QuickCode.Demo.ApartmentManageModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseType
    {
        public static class Query
        {
            private const string _prefix = "ApartmentManageModule.ExpenseType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetActiveExpenseTypes => ResourceKey("GetActiveExpenseTypes.g.sql");
            public static string GetCommonExpensesForExpenseTypes => ResourceKey("GetCommonExpensesForExpenseTypes.g.sql");
            public static string GetCommonExpensesForExpenseTypesDetails => ResourceKey("GetCommonExpensesForExpenseTypesDetails.g.sql");
        }
    }
}