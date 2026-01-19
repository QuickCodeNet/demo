SELECT [Key], [ModuleName], [ModelName], [PageAction], [PagePath] 
FROM [PortalPageDefinitions] 
WHERE [ModelName] = @PRM_PortalPageDefinitions_ModelName 
ORDER BY [Key] 