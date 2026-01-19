SELECT P.[Name], P.[Description] 
FROM [ApiMethodAccessGrants] A 
	INNER JOIN [PermissionGroups] P 
			ON A.[PermissionGroupName] = P.[Name] 
WHERE P.[Name] = @PRM_PermissionGroups_Name 
ORDER BY P.[Name] 