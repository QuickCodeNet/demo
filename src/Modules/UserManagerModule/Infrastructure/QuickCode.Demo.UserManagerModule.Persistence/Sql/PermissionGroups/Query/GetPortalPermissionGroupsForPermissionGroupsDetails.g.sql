SELECT P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId], P.[IsActive] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PermissionGroups] P2 
			ON P.[PermissionGroupName] = P2.[Name] 
WHERE P.[PortalPermissionName] = @PRM_PortalPermissionGroups_PortalPermissionName 
	AND P2.[Name] = @PRM_PermissionGroups_Name 
ORDER BY P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId] 