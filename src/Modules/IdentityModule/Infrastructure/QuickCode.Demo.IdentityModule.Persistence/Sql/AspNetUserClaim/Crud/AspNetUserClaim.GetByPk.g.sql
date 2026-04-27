SELECT
    [Id],
    [UserId],
    [ClaimType],
    [ClaimValue]
FROM [dbo].[AspNetUserClaims]
WHERE
    [Id] = @PRM_ASP_NET_USER_CLAIM_ID;