SELECT
    [ID],
    [ORDER_ID],
    [PRODUCT_VARIANT_ID],
    [PRODUCT_NAME],
    [SKU],
    [QUANTITY],
    [UNIT_PRICE],
    [TOTAL_PRICE]
FROM [dbo].[ORDER_ITEMS]
WHERE
    [ID] = @PRM_ORDER_ITEM_ID
    AND [IsDeleted] = 0;