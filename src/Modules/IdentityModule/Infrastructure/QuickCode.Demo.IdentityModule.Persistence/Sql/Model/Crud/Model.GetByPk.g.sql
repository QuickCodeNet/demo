SELECT
    [Name],
    [ModuleName],
    [Description]
FROM [dbo].[Models]
WHERE
    [Name] = @PRM_MODEL_NAME AND
    [ModuleName] = @PRM_MODEL_MODULE_NAME;