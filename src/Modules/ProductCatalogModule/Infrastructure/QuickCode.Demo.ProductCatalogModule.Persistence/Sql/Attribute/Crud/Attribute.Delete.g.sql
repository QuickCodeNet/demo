UPDATE [dbo].[ATTRIBUTES]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_ATTRIBUTE_ID
    AND [IsDeleted] = 0;