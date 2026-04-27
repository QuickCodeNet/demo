SELECT
    [Id],
    [FirstName],
    [LastName],
    [PermissionGroupName],
    [UserName],
    [NormalizedUserName],
    [Email],
    [NormalizedEmail],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [ConcurrencyStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEnd],
    [LockoutEnabled],
    [AccessFailedCount]
FROM [dbo].[AspNetUsers]
ORDER BY
    [Id]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;