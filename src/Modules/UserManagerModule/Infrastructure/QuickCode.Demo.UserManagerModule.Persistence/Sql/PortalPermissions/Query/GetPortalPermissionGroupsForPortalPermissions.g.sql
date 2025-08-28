SELECT P2.[Name], P2.[Description] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissions] P2 
			ON P.[PortalPermissionName] = P2.[Name] 
WHERE P2.[Name] = @PRM_PortalPermissions_Name 
ORDER BY P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId] 