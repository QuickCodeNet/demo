UPDATE [dbo].[TopicWorkflows]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [Id] = @PRM_TOPIC_WORKFLOW_ID
    AND [IsDeleted] = 0;