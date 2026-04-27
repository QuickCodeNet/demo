DELETE FROM [dbo].[KafkaEvents]
WHERE
    [TopicName] = @PRM_KAFKA_EVENT_TOPIC_NAME;