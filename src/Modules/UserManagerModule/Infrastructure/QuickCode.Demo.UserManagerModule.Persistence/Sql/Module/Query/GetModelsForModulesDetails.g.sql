SELECT M.[Name], M.[ModuleName], M.[Description] 
FROM [Models] M 
	INNER JOIN [Modules] M2 
			ON M.[ModuleName] = M2.[Name] 
WHERE M.[Name] = @PRM_Models_Name 
	AND M2.[Name] = @PRM_Modules_Name 
ORDER BY M.[Name], M.[ModuleName] 