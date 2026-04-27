INSERT INTO [dbo].[PermissionGroups] (
    [Name],
    [Description]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_PERMISSION_GROUP_NAME,
    @PRM_PERMISSION_GROUP_DESCRIPTION
    );