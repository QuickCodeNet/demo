SELECT A.[Id], A.[FirstName], A.[LastName], A.[PermissionGroupName], A.[UserName], A.[NormalizedUserName], A.[Email], A.[NormalizedEmail], A.[EmailConfirmed], A.[PasswordHash], A.[SecurityStamp], A.[ConcurrencyStamp], A.[PhoneNumber], A.[PhoneNumberConfirmed], A.[TwoFactorEnabled], A.[LockoutEnd], A.[LockoutEnabled], A.[AccessFailedCount] 
FROM [AspNetUsers] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupName] = P.[Name] 
WHERE A.[Id] = @PRM_AspNetUsers_Id 
	AND P.[Name] = @PRM_PermissionGroups_Name 
ORDER BY A.[Id] 