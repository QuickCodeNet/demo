INSERT INTO [dbo].[AspNetUserTokens] (
    [UserId],
    [LoginProvider],
    [Name],
    [Value]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_ASP_NET_USER_TOKEN_USER_ID,
    @PRM_ASP_NET_USER_TOKEN_LOGIN_PROVIDER,
    @PRM_ASP_NET_USER_TOKEN_NAME,
    @PRM_ASP_NET_USER_TOKEN_VALUE
    );