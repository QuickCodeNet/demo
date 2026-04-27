UPDATE [dbo].[PAYOUT_LINE_ITEMS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_PAYOUT_LINE_ITEM_ID
    AND [IsDeleted] = 0;