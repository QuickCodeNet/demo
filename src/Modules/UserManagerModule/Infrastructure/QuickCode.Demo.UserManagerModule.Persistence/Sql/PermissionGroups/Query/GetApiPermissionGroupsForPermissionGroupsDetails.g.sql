SELECT A.[Id], A.[PermissionGroupId], A.[ApiMethodDefinitionId] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupId] = P.[Id] 
WHERE A.[IsDeleted] = 0 
	AND P.[IsDeleted] = 0 
	AND A.[Id] = @PRM_ApiPermissionGroups_Id 
	AND P.[Id] = @PRM_PermissionGroups_Id 
ORDER BY A.[Id] 