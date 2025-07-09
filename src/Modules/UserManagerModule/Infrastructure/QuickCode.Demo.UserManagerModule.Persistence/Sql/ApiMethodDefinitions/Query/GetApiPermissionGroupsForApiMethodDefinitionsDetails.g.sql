SELECT A.[Id], A.[PermissionGroupId], A.[ApiMethodDefinitionId] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [ApiMethodDefinitions] A2 
			ON A.[ApiMethodDefinitionId] = A2.[Id] 
WHERE A.[IsDeleted] = 0 
	AND A2.[IsDeleted] = 0 
	AND A.[Id] = @PRM_ApiPermissionGroups_Id 
	AND A2.[Id] = @PRM_ApiMethodDefinitions_Id 
ORDER BY A.[Id] 