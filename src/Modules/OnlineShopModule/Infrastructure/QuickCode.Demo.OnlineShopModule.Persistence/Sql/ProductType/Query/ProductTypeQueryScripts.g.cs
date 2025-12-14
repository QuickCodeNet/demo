namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductType
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.ProductType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetProductTypes => ResourceKey("GetProductTypes.g.sql");
            public static string GetProductGroupsForProductTypes => ResourceKey("GetProductGroupsForProductTypes.g.sql");
            public static string GetProductGroupsForProductTypesDetails => ResourceKey("GetProductGroupsForProductTypesDetails.g.sql");
        }
    }
}