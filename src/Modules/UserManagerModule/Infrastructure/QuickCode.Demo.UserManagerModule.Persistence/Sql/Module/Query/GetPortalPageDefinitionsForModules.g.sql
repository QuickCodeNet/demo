SELECT M.[Name], M.[Description] 
FROM [PortalPageDefinitions] P 
	INNER JOIN [Modules] M 
			ON P.[ModuleName] = M.[Name] 
WHERE M.[Name] = @PRM_Modules_Name 
ORDER BY M.[Name] 