UPDATE [RefreshTokens] 
	SET [IsRevoked] = true 
WHERE [IsDeleted] = 0 
	AND [Token] = @PRM_RefreshTokens_Token