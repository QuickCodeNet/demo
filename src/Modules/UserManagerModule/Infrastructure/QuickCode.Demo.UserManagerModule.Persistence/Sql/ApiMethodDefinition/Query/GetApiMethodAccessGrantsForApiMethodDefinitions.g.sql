SELECT A2.[Key], A2.[ModuleName], A2.[ModelName], A2.[HttpMethod], A2.[ControllerName], A2.[MethodName], A2.[UrlPath] 
FROM [ApiMethodAccessGrants] A 
	INNER JOIN [ApiMethodDefinitions] A2 
			ON A.[ApiMethodDefinitionKey] = A2.[Key] 
WHERE A2.[Key] = @PRM_ApiMethodDefinitions_Key 
ORDER BY A2.[Key] 