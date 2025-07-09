SELECT [Id], [PortalPermissionId], [PermissionGroupId], [PortalPermissionTypeId] 
FROM [PortalPermissionGroups] 
WHERE [IsDeleted] = 0 
	AND [PortalPermissionId] = @PRM_PortalPermissionGroups_PortalPermissionId 
	AND [PermissionGroupId] = @PRM_PortalPermissionGroups_PermissionGroupId 
	AND [PortalPermissionTypeId] = @PRM_PortalPermissionGroups_PortalPermissionTypeId 
ORDER BY [Id] 