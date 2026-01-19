SELECT [Key], [ModuleName], [ModelName], [HttpMethod], [ControllerName], [MethodName], [UrlPath] 
FROM [ApiMethodDefinitions] 
WHERE [ModelName] = @PRM_ApiMethodDefinitions_ModelName 
ORDER BY [Key] 