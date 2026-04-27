UPDATE [dbo].[TopicWorkflows]
SET
    [KafkaEventsTopicName] = @PRM_TOPIC_WORKFLOW_KAFKA_EVENTS_TOPIC_NAME,
    [WorkflowContent] = @PRM_TOPIC_WORKFLOW_WORKFLOW_CONTENT
WHERE
    [Id] = @PRM_TOPIC_WORKFLOW_ID
    AND [IsDeleted] = 0;