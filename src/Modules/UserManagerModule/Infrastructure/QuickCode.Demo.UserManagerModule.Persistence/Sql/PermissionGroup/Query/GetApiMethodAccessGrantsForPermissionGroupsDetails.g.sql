SELECT A.[PermissionGroupName], A.[ApiMethodDefinitionKey], A.[ModifiedBy], A.[IsActive] 
FROM [ApiMethodAccessGrants] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupName] = P.[Name] 
WHERE A.[PermissionGroupName] = @PRM_ApiMethodAccessGrants_PermissionGroupName 
	AND P.[Name] = @PRM_PermissionGroups_Name 
ORDER BY A.[PermissionGroupName], A.[ApiMethodDefinitionKey] 