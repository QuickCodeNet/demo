SELECT P.[Id], P.[PortalPermissionId], P.[PermissionGroupId], P.[PortalPermissionTypeId] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PermissionGroups] P2 
			ON P.[PermissionGroupId] = P2.[Id] 
WHERE P.[IsDeleted] = 0 
	AND P2.[IsDeleted] = 0 
	AND P.[Id] = @PRM_PortalPermissionGroups_Id 
	AND P2.[Id] = @PRM_PermissionGroups_Id 
ORDER BY P.[Id] 