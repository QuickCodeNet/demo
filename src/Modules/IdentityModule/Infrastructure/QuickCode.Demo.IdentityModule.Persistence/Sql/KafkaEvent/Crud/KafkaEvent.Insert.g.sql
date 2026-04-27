INSERT INTO [dbo].[KafkaEvents] (
    [TopicName],
    [ApiMethodDefinitionKey],
    [IsActive]
)
OUTPUT INSERTED.*
VALUES (
    @PRM_KAFKA_EVENT_TOPIC_NAME,
    @PRM_KAFKA_EVENT_API_METHOD_DEFINITION_KEY,
    @PRM_KAFKA_EVENT_IS_ACTIVE
    );