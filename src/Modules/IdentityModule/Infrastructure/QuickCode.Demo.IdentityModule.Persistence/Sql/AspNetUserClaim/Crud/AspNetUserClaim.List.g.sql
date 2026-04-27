SELECT
    [Id],
    [UserId],
    [ClaimType],
    [ClaimValue]
FROM [dbo].[AspNetUserClaims]
ORDER BY
    [Id]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;