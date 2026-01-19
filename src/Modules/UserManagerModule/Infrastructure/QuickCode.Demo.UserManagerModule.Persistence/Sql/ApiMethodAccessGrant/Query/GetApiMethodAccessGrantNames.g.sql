SELECT [PermissionGroupName], [ApiMethodDefinitionKey], [ModifiedBy], [IsActive] 
FROM [ApiMethodAccessGrants] 
WHERE [PermissionGroupName] = @PRM_ApiMethodAccessGrants_PermissionGroupName 
	AND [IsActive] = '1' 
ORDER BY [PermissionGroupName], [ApiMethodDefinitionKey] 