SELECT P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId], P.[IsActive] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissionTypes] P2 
			ON P.[PortalPermissionTypeId] = P2.[Id] 
WHERE P.[PortalPermissionName] = @PRM_PortalPermissionGroups_PortalPermissionName 
	AND P2.[Id] = @PRM_PortalPermissionTypes_Id 
ORDER BY P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId] 