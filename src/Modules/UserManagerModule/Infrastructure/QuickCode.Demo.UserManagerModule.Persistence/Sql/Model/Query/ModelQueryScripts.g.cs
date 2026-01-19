namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Model
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.Model.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ModuleNameIsExists => ResourceKey("ModuleNameIsExists.g.sql");
            public static string GetApiMethodDefinitionsForModels => ResourceKey("GetApiMethodDefinitionsForModels.g.sql");
            public static string GetApiMethodDefinitionsForModelsDetails => ResourceKey("GetApiMethodDefinitionsForModelsDetails.g.sql");
            public static string GetPortalPageDefinitionsForModels => ResourceKey("GetPortalPageDefinitionsForModels.g.sql");
            public static string GetPortalPageDefinitionsForModelsDetails => ResourceKey("GetPortalPageDefinitionsForModelsDetails.g.sql");
        }
    }
}