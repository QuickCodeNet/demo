SELECT M.[Name], M.[ModuleName], M.[Description] 
FROM [PortalPageDefinitions] P 
	INNER JOIN [Models] M 
			ON P.[ModelName] = M.[Name] 
WHERE M.[Name] = @PRM_Models_Name 
ORDER BY M.[Name], M.[ModuleName] 