SELECT
    [PermissionGroupName],
    [PortalPageDefinitionKey],
    [PageAction],
    [ModifiedBy],
    [IsActive]
FROM [dbo].[PortalPageAccessGrants]
ORDER BY
    [PermissionGroupName],
    [PortalPageDefinitionKey],
    [PageAction]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;