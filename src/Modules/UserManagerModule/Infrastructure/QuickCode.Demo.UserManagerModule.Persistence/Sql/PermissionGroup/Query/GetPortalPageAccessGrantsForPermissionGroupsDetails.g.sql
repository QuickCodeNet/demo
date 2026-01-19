SELECT P.[PermissionGroupName], P.[PortalPageDefinitionKey], P.[PageAction], P.[ModifiedBy], P.[IsActive] 
FROM [PortalPageAccessGrants] P 
	INNER JOIN [PermissionGroups] P2 
			ON P.[PermissionGroupName] = P2.[Name] 
WHERE P.[PermissionGroupName] = @PRM_PortalPageAccessGrants_PermissionGroupName 
	AND P2.[Name] = @PRM_PermissionGroups_Name 
ORDER BY P.[PermissionGroupName], P.[PortalPageDefinitionKey], P.[PageAction] 