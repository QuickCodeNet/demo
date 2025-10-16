SELECT K.[TopicName], K.[ApiMethodDefinitionKey], K.[IsActive] 
FROM [TopicWorkflows] T 
	INNER JOIN [KafkaEvents] K 
			ON T.[KafkaEventsTopicName] = K.[TopicName] 
WHERE T.[IsDeleted] = 0 
	AND K.[TopicName] = @PRM_KafkaEvents_TopicName 
ORDER BY K.[TopicName] 