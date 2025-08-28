SELECT [PermissionGroupName], [ApiMethodDefinitionKey], [IsActive] 
FROM [ApiPermissionGroups] 
WHERE [PermissionGroupName] = @PRM_ApiPermissionGroups_PermissionGroupName 
	AND [IsActive] = true 
ORDER BY [PermissionGroupName], [ApiMethodDefinitionKey] 