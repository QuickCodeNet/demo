SELECT [PermissionGroupName], [ApiMethodDefinitionKey], [IsActive] 
FROM [ApiPermissionGroups] 
WHERE [PermissionGroupName] = @PRM_ApiPermissionGroups_PermissionGroupName 
	AND [IsActive] = '1' 
ORDER BY [PermissionGroupName], [ApiMethodDefinitionKey] 