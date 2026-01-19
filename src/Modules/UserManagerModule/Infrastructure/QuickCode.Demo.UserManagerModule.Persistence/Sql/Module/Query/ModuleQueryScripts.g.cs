namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Module
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.Module.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ModuleNameIsExists => ResourceKey("ModuleNameIsExists.g.sql");
            public static string GetModelsForModules => ResourceKey("GetModelsForModules.g.sql");
            public static string GetModelsForModulesDetails => ResourceKey("GetModelsForModulesDetails.g.sql");
            public static string GetApiMethodDefinitionsForModules => ResourceKey("GetApiMethodDefinitionsForModules.g.sql");
            public static string GetApiMethodDefinitionsForModulesDetails => ResourceKey("GetApiMethodDefinitionsForModulesDetails.g.sql");
            public static string GetPortalPageDefinitionsForModules => ResourceKey("GetPortalPageDefinitionsForModules.g.sql");
            public static string GetPortalPageDefinitionsForModulesDetails => ResourceKey("GetPortalPageDefinitionsForModulesDetails.g.sql");
        }
    }
}