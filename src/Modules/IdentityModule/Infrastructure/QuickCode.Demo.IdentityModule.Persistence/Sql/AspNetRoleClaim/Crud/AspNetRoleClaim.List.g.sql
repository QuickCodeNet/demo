SELECT
    [Id],
    [RoleId],
    [ClaimType],
    [ClaimValue]
FROM [dbo].[AspNetRoleClaims]
ORDER BY
    [Id]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;