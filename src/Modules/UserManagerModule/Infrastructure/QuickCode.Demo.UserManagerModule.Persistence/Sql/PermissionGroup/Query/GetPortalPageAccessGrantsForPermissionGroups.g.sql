SELECT P2.[Name], P2.[Description] 
FROM [PortalPageAccessGrants] P 
	INNER JOIN [PermissionGroups] P2 
			ON P.[PermissionGroupName] = P2.[Name] 
WHERE P2.[Name] = @PRM_PermissionGroups_Name 
ORDER BY P2.[Name] 