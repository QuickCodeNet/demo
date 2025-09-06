SELECT P2.[Name], P2.[Description] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PermissionGroups] P2 
			ON P.[PermissionGroupName] = P2.[Name] 
WHERE P2.[Name] = @PRM_PermissionGroups_Name 
ORDER BY P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId] 