INSERT INTO [dbo].[AspNetRoleClaims] (
    [RoleId],
    [ClaimType],
    [ClaimValue]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_ASP_NET_ROLE_CLAIM_ROLE_ID,
    @PRM_ASP_NET_ROLE_CLAIM_CLAIM_TYPE,
    @PRM_ASP_NET_ROLE_CLAIM_CLAIM_VALUE
    );