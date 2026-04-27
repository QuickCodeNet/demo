SELECT
    [LoginProvider],
    [ProviderKey],
    [ProviderDisplayName],
    [UserId]
FROM [dbo].[AspNetUserLogins]
ORDER BY
    [LoginProvider],
    [ProviderKey]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;