SELECT A.[Key], A.[ModuleName], A.[ModelName], A.[HttpMethod], A.[ControllerName], A.[MethodName], A.[UrlPath] 
FROM [ApiMethodDefinitions] A 
	INNER JOIN [Models] M 
			ON A.[ModelName] = M.[Name] 
WHERE A.[Key] = @PRM_ApiMethodDefinitions_Key 
	AND M.[Name] = @PRM_Models_Name 
ORDER BY A.[Key] 