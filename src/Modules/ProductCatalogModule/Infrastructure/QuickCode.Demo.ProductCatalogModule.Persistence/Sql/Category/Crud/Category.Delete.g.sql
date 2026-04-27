UPDATE [dbo].[CATEGORIES]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_CATEGORY_ID
    AND [IsDeleted] = 0;