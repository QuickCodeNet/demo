SELECT
    [ID],
    [SELLER_ID],
    [NAME],
    [SLUG],
    [DESCRIPTION],
    [LOGO_URL],
    [IS_ACTIVE]
FROM [dbo].[STORES]
WHERE
    [ID] = @PRM_STORE_ID
    AND [IsDeleted] = 0;