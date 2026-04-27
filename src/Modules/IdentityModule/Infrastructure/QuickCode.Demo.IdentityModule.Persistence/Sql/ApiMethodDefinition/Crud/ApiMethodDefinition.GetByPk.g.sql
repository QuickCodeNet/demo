SELECT
    [Key],
    [ModuleName],
    [ModelName],
    [HttpMethod],
    [ControllerName],
    [MethodName],
    [UrlPath]
FROM [dbo].[ApiMethodDefinitions]
WHERE
    [Key] = @PRM_API_METHOD_DEFINITION_KEY;