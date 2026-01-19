namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPageDefinition
    {
        public static class Command
        {
            private const string _prefix = "UserManagerModule.PortalPageDefinition.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string DeletePortalPageDefinitionsWithModuleName => ResourceKey("DeletePortalPageDefinitionsWithModuleName.g.sql");
            public static string DeletePortalPageDefinitionsWithModelName => ResourceKey("DeletePortalPageDefinitionsWithModelName.g.sql");
        }
    }
}