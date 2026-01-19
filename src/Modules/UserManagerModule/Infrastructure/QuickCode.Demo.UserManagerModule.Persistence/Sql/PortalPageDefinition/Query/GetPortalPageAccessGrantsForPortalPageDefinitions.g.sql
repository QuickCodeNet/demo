SELECT P2.[Key], P2.[ModuleName], P2.[ModelName], P2.[PageAction], P2.[PagePath] 
FROM [PortalPageAccessGrants] P 
	INNER JOIN [PortalPageDefinitions] P2 
			ON P.[PortalPageDefinitionKey] = P2.[Key] 
WHERE P2.[Key] = @PRM_PortalPageDefinitions_Key 
ORDER BY P2.[Key] 