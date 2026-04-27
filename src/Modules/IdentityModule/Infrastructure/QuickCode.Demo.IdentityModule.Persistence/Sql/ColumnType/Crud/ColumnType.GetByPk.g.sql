SELECT
    [Id],
    [TypeName],
    [IosComponentName],
    [IosType],
    [IconCode]
FROM [dbo].[ColumnTypes]
WHERE
    [Id] = @PRM_COLUMN_TYPE_ID
    AND [IsDeleted] = 0;