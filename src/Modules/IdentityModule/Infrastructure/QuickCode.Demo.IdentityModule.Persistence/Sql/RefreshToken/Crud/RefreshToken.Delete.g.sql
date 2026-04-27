UPDATE [dbo].[RefreshTokens]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [Id] = @PRM_REFRESH_TOKEN_ID
    AND [IsDeleted] = 0;