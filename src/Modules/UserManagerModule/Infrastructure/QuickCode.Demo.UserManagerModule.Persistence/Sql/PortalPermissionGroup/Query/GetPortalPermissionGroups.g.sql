SELECT [PortalPermissionName], [PermissionGroupName], [PortalPermissionTypeId], [IsActive] 
FROM [PortalPermissionGroups] 
WHERE [PermissionGroupName] = @PRM_PortalPermissionGroups_PermissionGroupName 
	AND [IsActive] = '1' 
ORDER BY [PortalPermissionName], [PermissionGroupName], [PortalPermissionTypeId] 