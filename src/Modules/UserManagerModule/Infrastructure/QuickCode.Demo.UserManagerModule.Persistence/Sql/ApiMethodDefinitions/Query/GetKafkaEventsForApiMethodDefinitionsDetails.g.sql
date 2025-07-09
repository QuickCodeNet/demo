SELECT K.[TopicName], K.[ApiMethodDefinitionId], K.[IsActive] 
FROM [KafkaEvents] K 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionId] = A.[Id] 
WHERE A.[IsDeleted] = 0 
	AND K.[TopicName] = @PRM_KafkaEvents_TopicName 
	AND A.[Id] = @PRM_ApiMethodDefinitions_Id 
ORDER BY K.[TopicName] 