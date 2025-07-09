SELECT P2.[Id], P2.[Name] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PortalPermissionTypes] P2 
			ON P.[PortalPermissionTypeId] = P2.[Id] 
WHERE P.[IsDeleted] = 0 
	AND P2.[Id] = @PRM_PortalPermissionTypes_Id 
ORDER BY P.[Id] 