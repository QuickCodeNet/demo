SELECT
    [Name],
    [ModuleName],
    [Description]
FROM [dbo].[Models]
ORDER BY
    [Name],
    [ModuleName]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;