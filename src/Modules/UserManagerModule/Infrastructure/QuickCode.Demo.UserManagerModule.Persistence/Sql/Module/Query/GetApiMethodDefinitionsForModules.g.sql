SELECT M.[Name], M.[Description] 
FROM [ApiMethodDefinitions] A 
	INNER JOIN [Modules] M 
			ON A.[ModuleName] = M.[Name] 
WHERE M.[Name] = @PRM_Modules_Name 
ORDER BY M.[Name] 