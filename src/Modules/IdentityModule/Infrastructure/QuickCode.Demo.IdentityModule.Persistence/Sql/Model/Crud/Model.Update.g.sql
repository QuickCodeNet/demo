UPDATE [dbo].[Models]
SET
    [Description] = @PRM_MODEL_DESCRIPTION
WHERE
    [Name] = @PRM_MODEL_NAME AND
    [ModuleName] = @PRM_MODEL_MODULE_NAME;