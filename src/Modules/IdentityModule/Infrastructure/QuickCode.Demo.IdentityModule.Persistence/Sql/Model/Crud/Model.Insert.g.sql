INSERT INTO [dbo].[Models] (
    [Name],
    [ModuleName],
    [Description]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_MODEL_NAME,
    @PRM_MODEL_MODULE_NAME,
    @PRM_MODEL_DESCRIPTION
    );