UPDATE [dbo].[SHIPPING_METHODS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_SHIPPING_METHOD_ID
    AND [IsDeleted] = 0;