SELECT [Key], [ModuleName], [ModelName], [PageAction], [PagePath] 
FROM [PortalPageDefinitions] 
WHERE [ModuleName] = @PRM_PortalPageDefinitions_ModuleName 
ORDER BY [Key] 