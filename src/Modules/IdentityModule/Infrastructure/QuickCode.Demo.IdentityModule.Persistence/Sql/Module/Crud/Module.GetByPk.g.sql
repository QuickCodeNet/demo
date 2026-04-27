SELECT
    [Name],
    [Description]
FROM [dbo].[Modules]
WHERE
    [Name] = @PRM_MODULE_NAME;