SELECT P.[PermissionGroupName], P.[PortalPageDefinitionKey], P.[PageAction], P.[ModifiedBy], P.[IsActive] 
FROM [PortalPageAccessGrants] P 
	INNER JOIN [PortalPageDefinitions] P2 
			ON P.[PortalPageDefinitionKey] = P2.[Key] 
WHERE P.[PermissionGroupName] = @PRM_PortalPageAccessGrants_PermissionGroupName 
	AND P2.[Key] = @PRM_PortalPageDefinitions_Key 
ORDER BY P.[PermissionGroupName], P.[PortalPageDefinitionKey], P.[PageAction] 