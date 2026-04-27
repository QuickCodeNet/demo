SELECT
    [UserId],
    [LoginProvider],
    [Name],
    [Value]
FROM [dbo].[AspNetUserTokens]
WHERE
    [UserId] = @PRM_ASP_NET_USER_TOKEN_USER_ID;