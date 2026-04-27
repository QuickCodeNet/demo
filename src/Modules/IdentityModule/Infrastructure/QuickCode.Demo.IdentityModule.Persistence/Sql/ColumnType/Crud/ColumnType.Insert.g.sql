INSERT INTO [dbo].[ColumnTypes] (
    [TypeName],
    [IosComponentName],
    [IosType],
    [IconCode]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_COLUMN_TYPE_TYPE_NAME,
    @PRM_COLUMN_TYPE_IOS_COMPONENT_NAME,
    @PRM_COLUMN_TYPE_IOS_TYPE,
    @PRM_COLUMN_TYPE_ICON_CODE
    );