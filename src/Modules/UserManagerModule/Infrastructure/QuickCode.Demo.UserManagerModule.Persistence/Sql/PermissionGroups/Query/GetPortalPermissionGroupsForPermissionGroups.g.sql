SELECT P2.[Id], P2.[Name] 
FROM [PortalPermissionGroups] P 
	INNER JOIN [PermissionGroups] P2 
			ON P.[PermissionGroupId] = P2.[Id] 
WHERE P.[IsDeleted] = 0 
	AND P2.[IsDeleted] = 0 
	AND P2.[Id] = @PRM_PermissionGroups_Id 
ORDER BY P.[Id] 