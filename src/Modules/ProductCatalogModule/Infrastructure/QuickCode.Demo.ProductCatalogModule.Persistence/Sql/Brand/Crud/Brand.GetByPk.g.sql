SELECT
    [ID],
    [NAME],
    [LOGO_URL],
    [IS_ACTIVE]
FROM [dbo].[BRANDS]
WHERE
    [ID] = @PRM_BRAND_ID
    AND [IsDeleted] = 0;