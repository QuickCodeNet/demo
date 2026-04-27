SELECT
    [Id],
    [RoleId],
    [ClaimType],
    [ClaimValue]
FROM [dbo].[AspNetRoleClaims]
WHERE
    [Id] = @PRM_ASP_NET_ROLE_CLAIM_ID;