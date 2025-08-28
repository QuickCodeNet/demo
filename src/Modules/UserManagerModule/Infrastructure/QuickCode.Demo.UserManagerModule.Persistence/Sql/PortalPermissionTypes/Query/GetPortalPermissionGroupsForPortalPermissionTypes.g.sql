SELECT P2.[Id], P2.[Name], P2.[Description] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissionTypes] P2 
			ON P.[PortalPermissionTypeId] = P2.[Id] 
WHERE P2.[Id] = @PRM_PortalPermissionTypes_Id 
ORDER BY P.[PortalPermissionName], P.[PermissionGroupName], P.[PortalPermissionTypeId] 