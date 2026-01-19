namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class PortalPageAccessGrant
    {
        public static class Command
        {
            private const string _prefix = "UserManagerModule.PortalPageAccessGrant.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ClearPortalPageAccessGrants => ResourceKey("ClearPortalPageAccessGrants.g.sql");
        }
    }
}