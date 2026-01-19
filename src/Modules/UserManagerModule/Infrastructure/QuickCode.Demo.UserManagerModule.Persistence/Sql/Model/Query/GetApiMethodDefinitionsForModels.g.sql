SELECT M.[Name], M.[ModuleName], M.[Description] 
FROM [ApiMethodDefinitions] A 
	INNER JOIN [Models] M 
			ON A.[ModelName] = M.[Name] 
WHERE M.[Name] = @PRM_Models_Name 
ORDER BY M.[Name], M.[ModuleName] 