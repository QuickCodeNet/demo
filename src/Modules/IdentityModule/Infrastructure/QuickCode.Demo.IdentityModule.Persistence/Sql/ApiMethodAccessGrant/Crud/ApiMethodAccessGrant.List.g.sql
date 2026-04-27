SELECT
    [PermissionGroupName],
    [ApiMethodDefinitionKey],
    [ModifiedBy],
    [IsActive]
FROM [dbo].[ApiMethodAccessGrants]
ORDER BY
    [PermissionGroupName],
    [ApiMethodDefinitionKey]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;