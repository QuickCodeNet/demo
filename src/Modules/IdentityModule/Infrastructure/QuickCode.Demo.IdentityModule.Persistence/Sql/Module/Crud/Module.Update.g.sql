UPDATE [dbo].[Modules]
SET
    [Description] = @PRM_MODULE_DESCRIPTION
WHERE
    [Name] = @PRM_MODULE_NAME;