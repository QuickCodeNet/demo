SELECT
    [PermissionGroupName],
    [PortalPageDefinitionKey],
    [PageAction],
    [ModifiedBy],
    [IsActive]
FROM [dbo].[PortalPageAccessGrants]
WHERE
    [PermissionGroupName] = @PRM_PORTAL_PAGE_ACCESS_GRANT_PERMISSION_GROUP_NAME AND
    [PortalPageDefinitionKey] = @PRM_PORTAL_PAGE_ACCESS_GRANT_PORTAL_PAGE_DEFINITION_KEY AND
    [PageAction] = @PRM_PORTAL_PAGE_ACCESS_GRANT_PAGE_ACTION;