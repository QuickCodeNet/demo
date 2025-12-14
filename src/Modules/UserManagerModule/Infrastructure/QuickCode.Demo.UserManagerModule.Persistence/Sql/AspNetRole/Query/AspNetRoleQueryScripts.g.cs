namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class AspNetRole
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.AspNetRole.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetAspNetUserRolesForAspNetRoles => ResourceKey("GetAspNetUserRolesForAspNetRoles.g.sql");
            public static string GetAspNetUserRolesForAspNetRolesDetails => ResourceKey("GetAspNetUserRolesForAspNetRolesDetails.g.sql");
            public static string GetAspNetRoleClaimsForAspNetRoles => ResourceKey("GetAspNetRoleClaimsForAspNetRoles.g.sql");
            public static string GetAspNetRoleClaimsForAspNetRolesDetails => ResourceKey("GetAspNetRoleClaimsForAspNetRolesDetails.g.sql");
        }
    }
}