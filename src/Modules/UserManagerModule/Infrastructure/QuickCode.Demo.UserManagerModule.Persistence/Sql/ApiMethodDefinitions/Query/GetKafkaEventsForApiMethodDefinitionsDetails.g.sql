SELECT K.[TopicName], K.[ApiMethodDefinitionKey], K.[IsActive] 
FROM [KafkaEvents] K 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionKey] = A.[Key] 
WHERE K.[TopicName] = @PRM_KafkaEvents_TopicName 
	AND A.[Key] = @PRM_ApiMethodDefinitions_Key 
ORDER BY K.[TopicName] 