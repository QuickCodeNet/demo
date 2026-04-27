UPDATE [dbo].[ORDER_ITEMS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_ORDER_ITEM_ID
    AND [IsDeleted] = 0;