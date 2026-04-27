SELECT
    [ID],
    [PARENT_CATEGORY_ID],
    [NAME],
    [SLUG],
    [IS_ACTIVE]
FROM [dbo].[CATEGORIES]
WHERE
    [ID] = @PRM_CATEGORY_ID
    AND [IsDeleted] = 0;