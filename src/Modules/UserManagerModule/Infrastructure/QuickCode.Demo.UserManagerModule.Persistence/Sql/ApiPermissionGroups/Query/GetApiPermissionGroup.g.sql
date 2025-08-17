SELECT [PermissionGroupName], [ApiMethodDefinitionKey], [IsActive] 
FROM [ApiPermissionGroups] 
WHERE [ApiMethodDefinitionKey] = @PRM_ApiPermissionGroups_ApiMethodDefinitionKey 
	AND [PermissionGroupName] = @PRM_ApiPermissionGroups_PermissionGroupName 
	AND [IsActive] = true 
ORDER BY [PermissionGroupName], [ApiMethodDefinitionKey] 