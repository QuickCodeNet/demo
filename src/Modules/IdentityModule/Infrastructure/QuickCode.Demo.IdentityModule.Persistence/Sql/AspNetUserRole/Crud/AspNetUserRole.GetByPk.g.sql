SELECT
    [UserId],
    [RoleId]
FROM [dbo].[AspNetUserRoles]
WHERE
    [UserId] = @PRM_ASP_NET_USER_ROLE_USER_ID AND
    [RoleId] = @PRM_ASP_NET_USER_ROLE_ROLE_ID;