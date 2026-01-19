SELECT [Key], [ModuleName], [ModelName], [HttpMethod], [ControllerName], [MethodName], [UrlPath] 
FROM [ApiMethodDefinitions] 
WHERE [ModuleName] = @PRM_ApiMethodDefinitions_ModuleName 
ORDER BY [Key] 