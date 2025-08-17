SELECT A2.[Key], A2.[HttpMethod], A2.[ControllerName], A2.[UrlPath] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [ApiMethodDefinitions] A2 
			ON A.[ApiMethodDefinitionKey] = A2.[Key] 
WHERE A2.[Key] = @PRM_ApiMethodDefinitions_Key 
ORDER BY A.[PermissionGroupName], A.[ApiMethodDefinitionKey] 