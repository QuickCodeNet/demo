SELECT CASE WHEN EXISTS (
SELECT 1 
FROM [PortalPageDefinitions] 
WHERE [ModuleName] = @PRM_PortalPageDefinitions_ModuleName ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END