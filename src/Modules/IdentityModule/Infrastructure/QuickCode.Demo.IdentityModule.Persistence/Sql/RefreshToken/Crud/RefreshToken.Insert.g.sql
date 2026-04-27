INSERT INTO [dbo].[RefreshTokens] (
    [UserId],
    [Token],
    [ExpiryDate],
    [CreatedDate],
    [IsRevoked]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_REFRESH_TOKEN_USER_ID,
    @PRM_REFRESH_TOKEN_TOKEN,
    @PRM_REFRESH_TOKEN_EXPIRY_DATE,
    @PRM_REFRESH_TOKEN_CREATED_DATE,
    @PRM_REFRESH_TOKEN_IS_REVOKED
    );