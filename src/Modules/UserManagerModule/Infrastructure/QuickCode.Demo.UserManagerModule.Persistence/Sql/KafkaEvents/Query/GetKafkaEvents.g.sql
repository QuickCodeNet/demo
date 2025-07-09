SELECT K.[TopicName], K.[ApiMethodDefinitionId], K.[IsActive], A.[HttpMethod], A.[ControllerName], A.[UrlPath] 
FROM [KafkaEvents] K 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionId] = A.[Id] 
ORDER BY K.[TopicName] 