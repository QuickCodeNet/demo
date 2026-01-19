SELECT A.[Id], A.[UserId], A.[ClaimType], A.[ClaimValue] 
FROM [AspNetUserClaims] A 
	INNER JOIN [AspNetUsers] A2 
			ON A.[UserId] = A2.[Id] 
WHERE A.[Id] = @PRM_AspNetUserClaims_Id 
	AND A2.[Id] = @PRM_AspNetUsers_Id 
ORDER BY A.[Id] 