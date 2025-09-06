SELECT A.[PermissionGroupName], A.[ApiMethodDefinitionKey], A.[IsActive] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupName] = P.[Name] 
WHERE A.[PermissionGroupName] = @PRM_ApiPermissionGroups_PermissionGroupName 
	AND P.[Name] = @PRM_PermissionGroups_Name 
ORDER BY A.[PermissionGroupName], A.[ApiMethodDefinitionKey] 