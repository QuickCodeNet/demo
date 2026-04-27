UPDATE [dbo].[BRANDS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_BRAND_ID
    AND [IsDeleted] = 0;