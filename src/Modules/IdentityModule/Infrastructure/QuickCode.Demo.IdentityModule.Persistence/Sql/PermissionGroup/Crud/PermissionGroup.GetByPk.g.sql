SELECT
    [Name],
    [Description]
FROM [dbo].[PermissionGroups]
WHERE
    [Name] = @PRM_PERMISSION_GROUP_NAME;