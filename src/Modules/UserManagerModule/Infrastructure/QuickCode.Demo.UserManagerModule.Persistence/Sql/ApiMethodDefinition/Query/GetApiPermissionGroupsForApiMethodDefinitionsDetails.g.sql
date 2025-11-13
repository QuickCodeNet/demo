SELECT A.[PermissionGroupName], A.[ApiMethodDefinitionKey], A.[IsActive] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [ApiMethodDefinitions] A2 
			ON A.[ApiMethodDefinitionKey] = A2.[Key] 
WHERE A.[PermissionGroupName] = @PRM_ApiPermissionGroups_PermissionGroupName 
	AND A2.[Key] = @PRM_ApiMethodDefinitions_Key 
ORDER BY A.[PermissionGroupName], A.[ApiMethodDefinitionKey] 