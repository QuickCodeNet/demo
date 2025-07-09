SELECT P.[Id], P.[Name] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupId] = P.[Id] 
WHERE A.[IsDeleted] = 0 
	AND P.[IsDeleted] = 0 
	AND P.[Id] = @PRM_PermissionGroups_Id 
ORDER BY A.[Id] 