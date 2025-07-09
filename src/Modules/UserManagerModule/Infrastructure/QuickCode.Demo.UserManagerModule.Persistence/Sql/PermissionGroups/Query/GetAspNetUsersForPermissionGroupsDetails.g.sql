SELECT A.[Id], A.[FirstName], A.[LastName], A.[PermissionGroupId], A.[UserName], A.[NormalizedUserName], A.[Email], A.[NormalizedEmail], A.[EmailConfirmed], A.[PasswordHash], A.[SecurityStamp], A.[ConcurrencyStamp], A.[PhoneNumber], A.[PhoneNumberConfirmed], A.[TwoFactorEnabled], A.[LockoutEnd], A.[LockoutEnabled], A.[AccessFailedCount] 
FROM [AspNetUsers] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupId] = P.[Id] 
WHERE P.[IsDeleted] = 0 
	AND A.[Id] = @PRM_AspNetUsers_Id 
	AND P.[Id] = @PRM_PermissionGroups_Id 
ORDER BY A.[Id] 