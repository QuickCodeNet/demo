UPDATE [dbo].[ORDERS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_ORDER_ID
    AND [IsDeleted] = 0;