namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseType
    {
        public static class Query
        {
            public const string GetActiveExpenseTypes = "ApartmentManageModule.ExpenseType.Query.GetActiveExpenseTypes.g.sql";
            public const string GetCommonExpensesForExpenseTypes = "ApartmentManageModule.ExpenseType.Query.GetCommonExpensesForExpenseTypes.g.sql";
            public const string GetCommonExpensesForExpenseTypesDetails = "ApartmentManageModule.ExpenseType.Query.GetCommonExpensesForExpenseTypesDetails.g.sql";
        }
    }
}