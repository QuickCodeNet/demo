SELECT
    [Id],
    [UserId],
    [Token],
    [ExpiryDate],
    [CreatedDate],
    [IsRevoked]
FROM [dbo].[RefreshTokens]
WHERE
    [Id] = @PRM_REFRESH_TOKEN_ID
    AND [IsDeleted] = 0;