UPDATE [dbo].[RETURN_REQUESTS]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_RETURN_REQUEST_ID
    AND [IsDeleted] = 0;