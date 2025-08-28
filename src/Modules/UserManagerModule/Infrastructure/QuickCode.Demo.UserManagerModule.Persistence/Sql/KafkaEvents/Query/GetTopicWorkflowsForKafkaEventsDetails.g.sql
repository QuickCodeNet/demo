SELECT T.[Id], T.[KafkaEventsTopicName], T.[WorkflowContent] 
FROM [TopicWorkflows] T 
	INNER JOIN [KafkaEvents] K 
			ON T.[KafkaEventsTopicName] = K.[TopicName] 
WHERE T.[IsDeleted] = 0 
	AND T.[Id] = @PRM_TopicWorkflows_Id 
	AND K.[TopicName] = @PRM_KafkaEvents_TopicName 
ORDER BY T.[Id] 