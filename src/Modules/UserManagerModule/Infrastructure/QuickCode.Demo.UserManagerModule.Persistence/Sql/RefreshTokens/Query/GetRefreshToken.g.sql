SELECT TOP 1 [Id], [UserId], [Token], [ExpiryDate], [CreatedDate], [IsRevoked] 
FROM [RefreshTokens] 
WHERE [IsDeleted] = 0 
	AND [Token] = @PRM_RefreshTokens_Token 
	AND [IsRevoked] = false 
	AND [ExpiryDate] > GETDATE() 
ORDER BY [Id] 