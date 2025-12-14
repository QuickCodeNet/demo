namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductGroup
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.ProductGroup.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetGroupsByType => ResourceKey("GetGroupsByType.g.sql");
            public static string GetProductGroupsByType => ResourceKey("GetProductGroupsByType.g.sql");
            public static string GetProductsForProductGroups => ResourceKey("GetProductsForProductGroups.g.sql");
            public static string GetProductsForProductGroupsDetails => ResourceKey("GetProductsForProductGroupsDetails.g.sql");
        }
    }
}