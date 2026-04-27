UPDATE [dbo].[SELLERS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_SELLER_ID
    AND [IsDeleted] = 0;