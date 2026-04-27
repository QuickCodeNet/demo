UPDATE [dbo].[SELLER_PAYOUTS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_SELLER_PAYOUT_ID
    AND [IsDeleted] = 0;