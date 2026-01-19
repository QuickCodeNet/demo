SELECT A.[UserId], A.[RoleId] 
FROM [AspNetUserRoles] A 
	INNER JOIN [AspNetRoles] A2 
			ON A.[RoleId] = A2.[Id] 
WHERE A.[UserId] = @PRM_AspNetUserRoles_UserId 
	AND A2.[Id] = @PRM_AspNetRoles_Id 
ORDER BY A.[UserId], A.[RoleId] 