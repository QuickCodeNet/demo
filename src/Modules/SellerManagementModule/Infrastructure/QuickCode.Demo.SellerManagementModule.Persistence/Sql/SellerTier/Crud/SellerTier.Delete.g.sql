UPDATE [dbo].[SELLER_TIERS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_SELLER_TIER_ID
    AND [IsDeleted] = 0;