SELECT TOP 1 [Id], [FirstName], [LastName], [PermissionGroupName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount] 
FROM [AspNetUsers] 
WHERE [Email] = @PRM_AspNetUsers_Email 
ORDER BY [Id] 