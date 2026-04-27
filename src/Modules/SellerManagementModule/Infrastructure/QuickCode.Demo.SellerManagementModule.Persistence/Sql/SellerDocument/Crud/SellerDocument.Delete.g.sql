UPDATE [dbo].[SELLER_DOCUMENTS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_SELLER_DOCUMENT_ID
    AND [IsDeleted] = 0;