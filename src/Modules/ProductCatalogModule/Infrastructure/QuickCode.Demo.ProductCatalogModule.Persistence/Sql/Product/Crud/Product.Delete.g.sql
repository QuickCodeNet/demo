UPDATE [dbo].[PRODUCTS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_PRODUCT_ID
    AND [IsDeleted] = 0;