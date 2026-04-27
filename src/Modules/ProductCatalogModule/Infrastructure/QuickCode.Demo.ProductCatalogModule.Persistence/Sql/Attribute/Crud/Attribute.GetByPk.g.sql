SELECT
    [ID],
    [NAME],
    [CODE]
FROM [dbo].[ATTRIBUTES]
WHERE
    [ID] = @PRM_ATTRIBUTE_ID
    AND [IsDeleted] = 0;