SELECT
    [ID],
    [USER_ID],
    [COMPANY_NAME],
    [CONTACT_EMAIL],
    [CONTACT_PHONE],
    [STATUS],
    [SELLER_TIER_ID],
    [JOINED_DATE]
FROM [dbo].[SELLERS]
WHERE
    [ID] = @PRM_SELLER_ID
    AND [IsDeleted] = 0;