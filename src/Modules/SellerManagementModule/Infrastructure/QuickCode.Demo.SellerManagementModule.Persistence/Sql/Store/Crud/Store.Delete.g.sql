UPDATE [dbo].[STORES]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_STORE_ID
    AND [IsDeleted] = 0;