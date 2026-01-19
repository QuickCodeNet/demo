SELECT [PermissionGroupName], [PortalPageDefinitionKey], [PageAction], [ModifiedBy], [IsActive] 
FROM [PortalPageAccessGrants] 
WHERE [PermissionGroupName] = @PRM_PortalPageAccessGrants_PermissionGroupName 
	AND [IsActive] = '1' 
ORDER BY [PermissionGroupName], [PortalPageDefinitionKey], [PageAction] 