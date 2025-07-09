SELECT [Id], [PortalPermissionId], [PermissionGroupId], [PortalPermissionTypeId] 
FROM [PortalPermissionGroups] 
WHERE [IsDeleted] = 0 
	AND [PermissionGroupId] = @PRM_PortalPermissionGroups_PermissionGroupId 
ORDER BY [Id] 