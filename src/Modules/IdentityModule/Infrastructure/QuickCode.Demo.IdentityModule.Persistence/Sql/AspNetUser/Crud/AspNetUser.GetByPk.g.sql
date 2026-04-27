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
WHERE
    [Id] = @PRM_ASP_NET_USER_ID;