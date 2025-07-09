SELECT A.[Id], A.[HttpMethod], A.[ControllerName], A.[UrlPath], A.[ItemType] 
FROM [KafkaEvents] K 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionId] = A.[Id] 
WHERE A.[IsDeleted] = 0 
	AND A.[Id] = @PRM_ApiMethodDefinitions_Id 
ORDER BY K.[TopicName] 