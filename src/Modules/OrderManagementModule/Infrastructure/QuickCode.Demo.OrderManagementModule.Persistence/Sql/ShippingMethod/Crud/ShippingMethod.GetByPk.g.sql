SELECT
    [ID],
    [NAME],
    [COST],
    [IS_ACTIVE]
FROM [dbo].[SHIPPING_METHODS]
WHERE
    [ID] = @PRM_SHIPPING_METHOD_ID
    AND [IsDeleted] = 0;