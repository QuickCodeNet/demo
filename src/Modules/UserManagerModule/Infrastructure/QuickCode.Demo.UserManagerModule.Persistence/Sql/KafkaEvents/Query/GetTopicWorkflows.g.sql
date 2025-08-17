SELECT T.[Id], T.[KafkaEventsTopicName], T.[WorkflowContent] 
FROM [TopicWorkflows] T 
	INNER JOIN [KafkaEvents] K 
			ON T.[KafkaEventsTopicName] = K.[TopicName] 
	INNER JOIN [ApiMethodDefinitions] A 
			ON K.[ApiMethodDefinitionKey] = A.[Key] 
WHERE T.[IsDeleted] = 0 
	AND K.[IsActive] = true 
	AND K.[TopicName] = @PRM_KafkaEvents_TopicName 
	AND A.[HttpMethod] = @PRM_ApiMethodDefinitions_HttpMethod 
ORDER BY K.[TopicName] 