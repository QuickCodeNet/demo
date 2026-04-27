SELECT
    [Id],
    [KafkaEventsTopicName],
    [WorkflowContent]
FROM [dbo].[TopicWorkflows]
WHERE
    [Id] = @PRM_TOPIC_WORKFLOW_ID
    AND [IsDeleted] = 0;