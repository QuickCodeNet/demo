SELECT [Id], [KafkaEventsTopicName], [WorkflowContent] 
FROM [TopicWorkflows] 
WHERE [IsDeleted] = 0 
	AND [KafkaEventsTopicName] = @PRM_TopicWorkflows_KafkaEventsTopicName 
ORDER BY [Id] 