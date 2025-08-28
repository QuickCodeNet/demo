SELECT [PortalPermissionName], [PermissionGroupName], [PortalPermissionTypeId], [IsActive] 
FROM [PortalPermissionGroups] 
WHERE [PortalPermissionName] = @PRM_PortalPermissionGroups_PortalPermissionName 
	AND [PermissionGroupName] = @PRM_PortalPermissionGroups_PermissionGroupName 
	AND [PortalPermissionTypeId] = @PRM_PortalPermissionGroups_PortalPermissionTypeId 
	AND [IsActive] = true 
ORDER BY [PortalPermissionName], [PermissionGroupName], [PortalPermissionTypeId] 