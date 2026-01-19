SELECT A.[UserId], A.[LoginProvider], A.[Name], A.[Value] 
FROM [AspNetUserTokens] A 
	INNER JOIN [AspNetUsers] A2 
			ON A.[UserId] = A2.[Id] 
WHERE A.[UserId] = @PRM_AspNetUserTokens_UserId 
	AND A2.[Id] = @PRM_AspNetUsers_Id 
ORDER BY A.[UserId] 