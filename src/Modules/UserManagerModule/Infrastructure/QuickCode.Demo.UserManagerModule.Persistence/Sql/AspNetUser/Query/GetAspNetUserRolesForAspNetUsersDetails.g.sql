SELECT A.[UserId], A.[RoleId] 
FROM [AspNetUserRoles] A 
	INNER JOIN [AspNetUsers] A2 
			ON A.[UserId] = A2.[Id] 
WHERE A.[UserId] = @PRM_AspNetUserRoles_UserId 
	AND A2.[Id] = @PRM_AspNetUsers_Id 
ORDER BY A.[UserId], A.[RoleId] 