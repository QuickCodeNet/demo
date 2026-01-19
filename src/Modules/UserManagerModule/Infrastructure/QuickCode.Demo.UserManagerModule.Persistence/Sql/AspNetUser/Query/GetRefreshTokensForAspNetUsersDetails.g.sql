SELECT R.[Id], R.[UserId], R.[Token], R.[ExpiryDate], R.[CreatedDate], R.[IsRevoked] 
FROM [RefreshTokens] R 
	INNER JOIN [AspNetUsers] A 
			ON R.[UserId] = A.[Id] 
WHERE R.[IsDeleted] = 0 
	AND R.[Id] = @PRM_RefreshTokens_Id 
	AND A.[Id] = @PRM_AspNetUsers_Id 
ORDER BY R.[Id] 