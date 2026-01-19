SELECT P.[Key], P.[ModuleName], P.[ModelName], P.[PageAction], P.[PagePath] 
FROM [PortalPageDefinitions] P 
	INNER JOIN [Modules] M 
			ON P.[ModuleName] = M.[Name] 
WHERE P.[Key] = @PRM_PortalPageDefinitions_Key 
	AND M.[Name] = @PRM_Modules_Name 
ORDER BY P.[Key] 