SELECT
    [TopicName],
    [ApiMethodDefinitionKey],
    [IsActive]
FROM [dbo].[KafkaEvents]
ORDER BY
    [TopicName]
OFFSET @PRM_QC_LIST_OFFSET ROWS
FETCH NEXT @PRM_QC_LIST_PAGE_SIZE ROWS ONLY;