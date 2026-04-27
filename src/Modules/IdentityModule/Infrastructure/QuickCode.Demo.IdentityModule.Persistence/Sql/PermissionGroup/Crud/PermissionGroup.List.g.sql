SELECT
    [Name],
    [Description]
FROM [dbo].[PermissionGroups]
ORDER BY
    [Name]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;