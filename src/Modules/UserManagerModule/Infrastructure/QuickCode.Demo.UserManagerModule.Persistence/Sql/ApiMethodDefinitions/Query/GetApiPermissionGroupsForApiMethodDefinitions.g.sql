SELECT A2.[Id], A2.[HttpMethod], A2.[ControllerName], A2.[UrlPath], A2.[ItemType] 
FROM [ApiPermissionGroups] A 
	INNER JOIN [ApiMethodDefinitions] A2 
			ON A.[ApiMethodDefinitionId] = A2.[Id] 
WHERE A.[IsDeleted] = 0 
	AND A2.[IsDeleted] = 0 
	AND A2.[Id] = @PRM_ApiMethodDefinitions_Id 
ORDER BY A.[Id] 