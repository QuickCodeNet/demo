UPDATE [dbo].[PAYOUT_PERIODS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_PAYOUT_PERIOD_ID
    AND [IsDeleted] = 0;