INSERT INTO [dbo].[TopicWorkflows] (
    [KafkaEventsTopicName],
    [WorkflowContent]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_TOPIC_WORKFLOW_KAFKA_EVENTS_TOPIC_NAME,
    @PRM_TOPIC_WORKFLOW_WORKFLOW_CONTENT
    );