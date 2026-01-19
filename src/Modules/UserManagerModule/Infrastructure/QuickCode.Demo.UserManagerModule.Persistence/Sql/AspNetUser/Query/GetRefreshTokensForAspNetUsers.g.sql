SELECT A.[Id], A.[FirstName], A.[LastName], A.[PermissionGroupName], A.[UserName], A.[NormalizedUserName], A.[Email], A.[NormalizedEmail], A.[EmailConfirmed], A.[PasswordHash], A.[SecurityStamp], A.[ConcurrencyStamp], A.[PhoneNumber], A.[PhoneNumberConfirmed], A.[TwoFactorEnabled], A.[LockoutEnd], A.[LockoutEnabled], A.[AccessFailedCount] 
FROM [RefreshTokens] R 
	INNER JOIN [AspNetUsers] A 
			ON R.[UserId] = A.[Id] 
WHERE R.[IsDeleted] = 0 
	AND A.[Id] = @PRM_AspNetUsers_Id 
ORDER BY A.[Id] 