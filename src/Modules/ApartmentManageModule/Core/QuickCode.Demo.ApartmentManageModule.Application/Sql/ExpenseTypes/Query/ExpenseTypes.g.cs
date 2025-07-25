namespace QuickCode.Demo.ApartmentManageModule.Application.Sql;
public static partial class SqlScripts
{
    public static partial class ExpenseTypes
    {
        public static class Query
        {
            public const string GetActiveExpenseTypes = "ApartmentManageModule.ExpenseTypes.Query.GetActiveExpenseTypes.g.sql";
            public const string GetCommonExpensesForExpenseTypes = "ApartmentManageModule.ExpenseTypes.Query.GetCommonExpensesForExpenseTypes.g.sql";
            public const string GetCommonExpensesForExpenseTypesDetails = "ApartmentManageModule.ExpenseTypes.Query.GetCommonExpensesForExpenseTypesDetails.g.sql";
        }
    }
}