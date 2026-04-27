SELECT
    [Id],
    [UserId],
    [Token],
    [ExpiryDate],
    [CreatedDate],
    [IsRevoked]
FROM [dbo].[RefreshTokens]
WHERE [IsDeleted] = 0
ORDER BY
    [Id]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;