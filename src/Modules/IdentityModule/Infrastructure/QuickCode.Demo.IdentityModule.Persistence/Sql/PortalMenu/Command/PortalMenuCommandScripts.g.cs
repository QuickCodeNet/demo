namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalMenu
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.PortalMenu.Command";
            private const string _sqlScriptStem = "PortalMenu";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string DeletePortalMenuItemsWithModuleName => ResourceKey($"{_sqlScriptStem}.DeletePortalMenuItemsWithModuleName.g.sql");
            public static string DeletePortalMenuItemsWithModelName => ResourceKey($"{_sqlScriptStem}.DeletePortalMenuItemsWithModelName.g.sql");
        }
    }
}