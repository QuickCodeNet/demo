SELECT
    [Key],
    [ModuleName],
    [ModelName],
    [PageAction],
    [PagePath]
FROM [dbo].[PortalPageDefinitions]
ORDER BY
    [Key]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;