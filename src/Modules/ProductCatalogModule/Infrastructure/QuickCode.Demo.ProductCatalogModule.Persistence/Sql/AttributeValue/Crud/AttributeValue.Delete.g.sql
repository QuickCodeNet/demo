UPDATE [dbo].[ATTRIBUTE_VALUES]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_ATTRIBUTE_VALUE_ID
    AND [IsDeleted] = 0;