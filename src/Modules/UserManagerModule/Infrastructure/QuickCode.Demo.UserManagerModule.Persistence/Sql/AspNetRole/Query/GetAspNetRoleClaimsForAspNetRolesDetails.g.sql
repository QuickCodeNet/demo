SELECT A.[Id], A.[RoleId], A.[ClaimType], A.[ClaimValue] 
FROM [AspNetRoleClaims] A 
	INNER JOIN [AspNetRoles] A2 
			ON A.[RoleId] = A2.[Id] 
WHERE A.[Id] = @PRM_AspNetRoleClaims_Id 
	AND A2.[Id] = @PRM_AspNetRoles_Id 
ORDER BY A.[Id] 