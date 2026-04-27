UPDATE [dbo].[RefreshTokens]
SET
    [UserId] = @PRM_REFRESH_TOKEN_USER_ID,
    [Token] = @PRM_REFRESH_TOKEN_TOKEN,
    [ExpiryDate] = @PRM_REFRESH_TOKEN_EXPIRY_DATE,
    [CreatedDate] = @PRM_REFRESH_TOKEN_CREATED_DATE,
    [IsRevoked] = @PRM_REFRESH_TOKEN_IS_REVOKED
WHERE
    [Id] = @PRM_REFRESH_TOKEN_ID
    AND [IsDeleted] = 0;