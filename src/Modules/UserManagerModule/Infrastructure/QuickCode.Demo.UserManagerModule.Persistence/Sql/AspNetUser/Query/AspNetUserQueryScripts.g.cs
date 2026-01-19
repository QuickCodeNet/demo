namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class AspNetUser
    {
        public static class Query
        {
            private const string _prefix = "UserManagerModule.AspNetUser.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetUser => ResourceKey("GetUser.g.sql");
            public static string GetRefreshTokensForAspNetUsers => ResourceKey("GetRefreshTokensForAspNetUsers.g.sql");
            public static string GetRefreshTokensForAspNetUsersDetails => ResourceKey("GetRefreshTokensForAspNetUsersDetails.g.sql");
            public static string GetAspNetUserRolesForAspNetUsers => ResourceKey("GetAspNetUserRolesForAspNetUsers.g.sql");
            public static string GetAspNetUserRolesForAspNetUsersDetails => ResourceKey("GetAspNetUserRolesForAspNetUsersDetails.g.sql");
            public static string GetAspNetUserClaimsForAspNetUsers => ResourceKey("GetAspNetUserClaimsForAspNetUsers.g.sql");
            public static string GetAspNetUserClaimsForAspNetUsersDetails => ResourceKey("GetAspNetUserClaimsForAspNetUsersDetails.g.sql");
            public static string GetAspNetUserLoginsForAspNetUsers => ResourceKey("GetAspNetUserLoginsForAspNetUsers.g.sql");
            public static string GetAspNetUserLoginsForAspNetUsersDetails => ResourceKey("GetAspNetUserLoginsForAspNetUsersDetails.g.sql");
            public static string GetAspNetUserTokensForAspNetUsers => ResourceKey("GetAspNetUserTokensForAspNetUsers.g.sql");
            public static string GetAspNetUserTokensForAspNetUsersDetails => ResourceKey("GetAspNetUserTokensForAspNetUsersDetails.g.sql");
        }
    }
}