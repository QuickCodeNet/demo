SELECT P.[Id], P.[PortalPermissionId], P.[PermissionGroupId], P.[PortalPermissionTypeId] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissionTypes] P2 
			ON P.[PortalPermissionTypeId] = P2.[Id] 
WHERE P.[IsDeleted] = 0 
	AND P.[Id] = @PRM_PortalPermissionGroups_Id 
	AND P2.[Id] = @PRM_PortalPermissionTypes_Id 
ORDER BY P.[Id] 