UPDATE [dbo].[COMMISSION_RULES]
SET [IsDeleted] = 1, [DeletedOnUtc] = SYSUTCDATETIME()
WHERE
    [ID] = @PRM_COMMISSION_RULE_ID
    AND [IsDeleted] = 0;