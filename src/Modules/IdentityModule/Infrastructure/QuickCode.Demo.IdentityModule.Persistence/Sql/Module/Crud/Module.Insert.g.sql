INSERT INTO [dbo].[Modules] (
    [Name],
    [Description]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_MODULE_NAME,
    @PRM_MODULE_DESCRIPTION
    );