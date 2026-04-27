INSERT INTO [dbo].[AspNetRoles] (
    [Id],
    [Name],
    [NormalizedName],
    [ConcurrencyStamp]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_ASP_NET_ROLE_ID,
    @PRM_ASP_NET_ROLE_NAME,
    @PRM_ASP_NET_ROLE_NORMALIZED_NAME,
    @PRM_ASP_NET_ROLE_CONCURRENCY_STAMP
    );