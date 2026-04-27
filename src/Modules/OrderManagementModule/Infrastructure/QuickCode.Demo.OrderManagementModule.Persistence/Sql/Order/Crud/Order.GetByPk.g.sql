SELECT
    [ID],
    [CUSTOMER_ID],
    [SELLER_ID],
    [ORDER_NUMBER],
    [TOTAL_AMOUNT],
    [SHIPPING_ADDRESS],
    [STATUS],
    [ORDER_DATE]
FROM [dbo].[ORDERS]
WHERE
    [ID] = @PRM_ORDER_ID
    AND [IsDeleted] = 0;