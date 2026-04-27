INSERT INTO [dbo].[AspNetUserRoles] (
    [UserId],
    [RoleId]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_ASP_NET_USER_ROLE_USER_ID,
    @PRM_ASP_NET_USER_ROLE_ROLE_ID
    );