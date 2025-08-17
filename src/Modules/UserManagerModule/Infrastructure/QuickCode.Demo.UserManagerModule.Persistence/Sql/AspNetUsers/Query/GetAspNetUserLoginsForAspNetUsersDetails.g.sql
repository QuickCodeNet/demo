SELECT A.[LoginProvider], A.[ProviderKey], A.[ProviderDisplayName], A.[UserId] 
FROM [AspNetUserLogins] A 
	INNER JOIN [AspNetUsers] A2 
			ON A.[UserId] = A2.[Id] 
WHERE A.[LoginProvider] = @PRM_AspNetUserLogins_LoginProvider 
	AND A2.[Id] = @PRM_AspNetUsers_Id 
ORDER BY A.[LoginProvider], A.[ProviderKey] 