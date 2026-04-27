SELECT
    [Id],
    [Name],
    [NormalizedName],
    [ConcurrencyStamp]
FROM [dbo].[AspNetRoles]
WHERE
    [Id] = @PRM_ASP_NET_ROLE_ID;