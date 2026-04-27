UPDATE [dbo].[ColumnTypes]
SET
    [TypeName] = @PRM_COLUMN_TYPE_TYPE_NAME,
    [IosComponentName] = @PRM_COLUMN_TYPE_IOS_COMPONENT_NAME,
    [IosType] = @PRM_COLUMN_TYPE_IOS_TYPE,
    [IconCode] = @PRM_COLUMN_TYPE_ICON_CODE
WHERE
    [Id] = @PRM_COLUMN_TYPE_ID
    AND [IsDeleted] = 0;