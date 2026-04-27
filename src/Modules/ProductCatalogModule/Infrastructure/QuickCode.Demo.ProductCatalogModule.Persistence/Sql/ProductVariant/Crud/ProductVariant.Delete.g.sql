UPDATE [dbo].[PRODUCT_VARIANTS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_PRODUCT_VARIANT_ID
    AND [IsDeleted] = 0;