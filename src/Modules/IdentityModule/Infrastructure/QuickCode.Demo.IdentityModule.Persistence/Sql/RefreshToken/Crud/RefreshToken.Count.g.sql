SELECT
    COUNT(*)
FROM [dbo].[RefreshTokens]
WHERE [IsDeleted] = 0;