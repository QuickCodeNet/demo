SELECT
    [UserId],
    [LoginProvider],
    [Name],
    [Value]
FROM [dbo].[AspNetUserTokens]
ORDER BY
    [UserId]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;