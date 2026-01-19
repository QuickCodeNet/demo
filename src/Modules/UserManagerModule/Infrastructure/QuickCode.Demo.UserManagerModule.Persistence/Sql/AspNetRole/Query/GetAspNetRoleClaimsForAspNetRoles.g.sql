SELECT A2.[Id], A2.[Name], A2.[NormalizedName], A2.[ConcurrencyStamp] 
FROM [AspNetRoleClaims] A 
	INNER JOIN [AspNetRoles] A2 
			ON A.[RoleId] = A2.[Id] 
WHERE A2.[Id] = @PRM_AspNetRoles_Id 
ORDER BY A2.[Id] 