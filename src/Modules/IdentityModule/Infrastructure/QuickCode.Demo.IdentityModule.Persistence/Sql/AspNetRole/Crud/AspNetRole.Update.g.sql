UPDATE [dbo].[AspNetRoles]
SET
    [Name] = @PRM_ASP_NET_ROLE_NAME,
    [NormalizedName] = @PRM_ASP_NET_ROLE_NORMALIZED_NAME,
    [ConcurrencyStamp] = @PRM_ASP_NET_ROLE_CONCURRENCY_STAMP
WHERE
    [Id] = @PRM_ASP_NET_ROLE_ID;