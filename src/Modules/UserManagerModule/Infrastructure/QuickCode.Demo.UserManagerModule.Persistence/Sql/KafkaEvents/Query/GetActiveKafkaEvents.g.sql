SELECT K.[TopicName], K.[ApiMethodDefinitionId], K.[IsActive], A.[HttpMethod], A.[ControllerName], A.[UrlPath] 
FROM [KafkaEvents] K 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionId] = A.[Id] 
WHERE A.[IsDeleted] = 0 
	AND K.[IsActive] = true 
ORDER BY K.[TopicName] 