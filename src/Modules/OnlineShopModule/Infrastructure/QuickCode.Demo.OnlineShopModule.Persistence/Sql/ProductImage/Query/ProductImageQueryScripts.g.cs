namespace QuickCode.Demo.OnlineShopModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class ProductImage
    {
        public static class Query
        {
            private const string _prefix = "OnlineShopModule.ProductImage.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetValidProductImages => ResourceKey("GetValidProductImages.g.sql");
        }
    }
}