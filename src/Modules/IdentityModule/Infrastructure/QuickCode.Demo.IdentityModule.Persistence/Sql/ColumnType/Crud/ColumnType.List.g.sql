SELECT
    [Id],
    [TypeName],
    [IosComponentName],
    [IosType],
    [IconCode]
FROM [dbo].[ColumnTypes]
WHERE [IsDeleted] = 0
ORDER BY
    [Id]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;