SELECT P2.[Id], P2.[Name], P2.[ItemType] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissions] P2 
			ON P.[PortalPermissionId] = P2.[Id] 
WHERE P.[IsDeleted] = 0 
	AND P2.[IsDeleted] = 0 
	AND P2.[Id] = @PRM_PortalPermissions_Id 
ORDER BY P.[Id] 