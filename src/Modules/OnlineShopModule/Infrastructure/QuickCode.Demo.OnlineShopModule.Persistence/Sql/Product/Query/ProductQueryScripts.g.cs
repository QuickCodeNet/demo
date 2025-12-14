namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Product
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.Product.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string SearchProducts => ResourceKey("SearchProducts.g.sql");
            public static string ListLowStock => ResourceKey("ListLowStock.g.sql");
            public static string GetProductImagesForProducts => ResourceKey("GetProductImagesForProducts.g.sql");
            public static string GetProductImagesForProductsDetails => ResourceKey("GetProductImagesForProductsDetails.g.sql");
            public static string GetProductReviewsForProducts => ResourceKey("GetProductReviewsForProducts.g.sql");
            public static string GetProductReviewsForProductsDetails => ResourceKey("GetProductReviewsForProductsDetails.g.sql");
            public static string GetCartItemsForProducts => ResourceKey("GetCartItemsForProducts.g.sql");
            public static string GetCartItemsForProductsDetails => ResourceKey("GetCartItemsForProductsDetails.g.sql");
            public static string GetOrderItemsForProducts => ResourceKey("GetOrderItemsForProducts.g.sql");
            public static string GetOrderItemsForProductsDetails => ResourceKey("GetOrderItemsForProductsDetails.g.sql");
        }
    }
}