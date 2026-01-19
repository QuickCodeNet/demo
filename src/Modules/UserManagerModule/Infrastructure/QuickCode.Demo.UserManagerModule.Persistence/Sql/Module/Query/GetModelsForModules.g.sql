SELECT M2.[Name], M2.[Description] 
FROM [Models] M 
	INNER JOIN [Modules] M2 
			ON M.[ModuleName] = M2.[Name] 
WHERE M2.[Name] = @PRM_Modules_Name 
ORDER BY M2.[Name] 