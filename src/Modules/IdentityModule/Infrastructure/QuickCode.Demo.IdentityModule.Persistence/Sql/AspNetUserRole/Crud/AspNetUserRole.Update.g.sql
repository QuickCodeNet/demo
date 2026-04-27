-- PK-only: relink = DELETE existing row, INSERT new row (same pattern as EF many-to-many).
DELETE FROM [dbo].[AspNetUserRoles]
WHERE
    [UserId] = @PRM_ASP_NET_USER_ROLE_USER_ID_REMOVE AND
    [RoleId] = @PRM_ASP_NET_USER_ROLE_ROLE_ID_REMOVE;

INSERT INTO [dbo].[AspNetUserRoles] (
    [UserId],
    [RoleId]
)
VALUES (
    @PRM_ASP_NET_USER_ROLE_USER_ID_ADD,
    @PRM_ASP_NET_USER_ROLE_ROLE_ID_ADD
);