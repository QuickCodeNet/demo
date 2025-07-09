SELECT P.[Id], P.[Name] 
FROM [AspNetUsers] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupId] = P.[Id] 
WHERE P.[IsDeleted] = 0 
	AND P.[Id] = @PRM_PermissionGroups_Id 
ORDER BY A.[Id] 