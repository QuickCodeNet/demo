SELECT
    [Key],
    [ModuleName],
    [ModelName],
    [PageAction],
    [PagePath]
FROM [dbo].[PortalPageDefinitions]
WHERE
    [Key] = @PRM_PORTAL_PAGE_DEFINITION_KEY;