SELECT [PortalPermissionName], [PermissionGroupName], [PortalPermissionTypeId], [IsActive] 
FROM [PortalPermissionGroups] 
WHERE [PermissionGroupName] = @PRM_PortalPermissionGroups_PermissionGroupName 
	AND [IsActive] = true 
ORDER BY [PortalPermissionName], [PermissionGroupName], [PortalPermissionTypeId] 