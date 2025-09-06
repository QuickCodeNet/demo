SELECT [PermissionGroupName], [ApiMethodDefinitionKey], [IsActive] 
FROM [ApiPermissionGroups] 
WHERE [ApiMethodDefinitionKey] = @PRM_ApiPermissionGroups_ApiMethodDefinitionKey 
	AND [PermissionGroupName] = @PRM_ApiPermissionGroups_PermissionGroupName 
	AND [IsActive] = '1' 
ORDER BY [PermissionGroupName], [ApiMethodDefinitionKey] 