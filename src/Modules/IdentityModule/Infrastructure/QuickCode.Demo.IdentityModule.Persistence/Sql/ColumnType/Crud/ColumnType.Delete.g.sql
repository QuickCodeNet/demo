UPDATE [dbo].[ColumnTypes]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [Id] = @PRM_COLUMN_TYPE_ID
    AND [IsDeleted] = 0;