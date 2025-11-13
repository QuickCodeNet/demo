SELECT P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId], P.[IsActive] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissions] P2 
			ON P.[PortalPermissionName] = P2.[Name] 
WHERE P.[PortalPermissionName] = @PRM_PortalPermissionGroups_PortalPermissionName 
	AND P2.[Name] = @PRM_PortalPermissions_Name 
ORDER BY P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId] 