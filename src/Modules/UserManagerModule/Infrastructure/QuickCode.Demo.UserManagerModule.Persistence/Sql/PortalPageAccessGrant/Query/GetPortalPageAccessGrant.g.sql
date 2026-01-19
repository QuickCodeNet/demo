SELECT [PermissionGroupName], [PortalPageDefinitionKey], [PageAction], [ModifiedBy], [IsActive] 
FROM [PortalPageAccessGrants] 
WHERE [PortalPageDefinitionKey] = @PRM_PortalPageAccessGrants_PortalPageDefinitionKey 
	AND [PermissionGroupName] = @PRM_PortalPageAccessGrants_PermissionGroupName 
	AND [PageAction] = @PRM_PortalPageAccessGrants_PageAction 
	AND [IsActive] = '1' 
ORDER BY [PermissionGroupName], [PortalPageDefinitionKey], [PageAction] 