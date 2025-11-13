SELECT A2.[Id], A2.[FirstName], A2.[LastName], A2.[PermissionGroupName], A2.[UserName], A2.[NormalizedUserName], A2.[Email], A2.[NormalizedEmail], A2.[EmailConfirmed], A2.[PasswordHash], A2.[SecurityStamp], A2.[ConcurrencyStamp], A2.[PhoneNumber], A2.[PhoneNumberConfirmed], A2.[TwoFactorEnabled], A2.[LockoutEnd], A2.[LockoutEnabled], A2.[AccessFailedCount] 
FROM [AspNetUserClaims] A 
	INNER JOIN [AspNetUsers] A2 
			ON A.[UserId] = A2.[Id] 
WHERE A2.[Id] = @PRM_AspNetUsers_Id 
ORDER BY A2.[Id] 