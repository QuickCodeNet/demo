SELECT P.[Key], P.[ModuleName], P.[ModelName], P.[PageAction], P.[PagePath] 
FROM [PortalPageDefinitions] P 
	INNER JOIN [Models] M 
			ON P.[ModelName] = M.[Name] 
WHERE P.[Key] = @PRM_PortalPageDefinitions_Key 
	AND M.[Name] = @PRM_Models_Name 
ORDER BY P.[Key] 