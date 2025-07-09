SELECT [Id], [PermissionGroupId], [ApiMethodDefinitionId] 
FROM [ApiPermissionGroups] 
WHERE [IsDeleted] = 0 
	AND [ApiMethodDefinitionId] = @PRM_ApiPermissionGroups_ApiMethodDefinitionId 
	AND [PermissionGroupId] = @PRM_ApiPermissionGroups_PermissionGroupId 
ORDER BY [Id] 