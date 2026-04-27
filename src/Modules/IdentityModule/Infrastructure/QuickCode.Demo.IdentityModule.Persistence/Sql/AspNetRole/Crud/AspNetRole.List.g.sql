SELECT
    [Id],
    [Name],
    [NormalizedName],
    [ConcurrencyStamp]
FROM [dbo].[AspNetRoles]
ORDER BY
    [Id]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;